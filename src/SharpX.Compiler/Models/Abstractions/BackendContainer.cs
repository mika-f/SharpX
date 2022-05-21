// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Reflection;

using Microsoft.CodeAnalysis;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Compiler.Models.Abstractions;

internal class BackendContainer
{
    private readonly SortedList<uint, Type> _visitors;
    private readonly List<string> _references;

    public string Language { get; }

    public Type ReturnType { get; }

    public Func<SyntaxNode, string>? ExtensionCallback { get; private set; }

    public IReadOnlyCollection<string> References => _references.AsReadOnly();

    public BackendContainer(string language, Type @return)
    {
        Language = language;
        ReturnType = @return;
        _visitors = new SortedList<uint, Type>();
        _references = new List<string>();
    }

    public void Register(uint priority, Type visitor)
    {
        _visitors.Add(priority, visitor);
    }

    public void AddReferences(string[] references)
    {
        _references.AddRange(references);
    }

    [MemberNotNull(nameof(ExtensionCallback))]
    public void AddExtensionCallback(Func<SyntaxNode, string> callback)
    {
        ExtensionCallback = callback;
    }

    public SyntaxNode? RunAsync(Microsoft.CodeAnalysis.SyntaxNode syntax, SemanticModel model)
    {
        try
        {
            var (instance, register, visit1, visit2) = CreateRootSyntaxVisitorInstance();
            var args = CreateBackendVisitorArgs(model, node => (SyntaxNode?)visit1.Invoke(instance, new object?[] { node }), (oldNode, newNode) => (SyntaxNode?)visit2.Invoke(instance, new object?[] { oldNode, newNode }));

            foreach (var visitor in _visitors)
            {
                var visitorInstance = Activator.CreateInstance(visitor.Value, args);
                register.Invoke(instance, new[] { visitorInstance });
            }


            return visit1.Invoke(instance, new object?[] { syntax }) as SyntaxNode;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();
        }

        return null;
    }

    public SyntaxNode? RunAsync(Microsoft.CodeAnalysis.SyntaxNode syntax, SemanticModel model, Func<string, Microsoft.CodeAnalysis.SyntaxNode?, SyntaxNode?> invoker)
    {
        try
        {
            var (instance, register, visit1, visit2) = CreateRootSyntaxVisitorInstance();
            var args = CreateBackendVisitorArgs(model, node => (SyntaxNode?)visit1.Invoke(instance, new object?[] { node }), (oldNode, newNode) => (SyntaxNode?)visit2.Invoke(instance, new object?[] { oldNode, newNode }), (language, node) => invoker.Invoke(language, node));

            foreach (var visitor in _visitors)
            {
                var visitorInstance = Activator.CreateInstance(visitor.Value, args);
                register.Invoke(instance, new[] { visitorInstance });
            }


            return visit1.Invoke(instance, new object?[] { syntax }) as SyntaxNode;
        }
        catch (Exception e)
        {
            if (Debugger.IsAttached)
                Debugger.Break();
        }

        return null;
    }

    private (object, MethodInfo, MethodInfo, MethodInfo) CreateRootSyntaxVisitorInstance()
    {
        var activator = typeof(RootCSharpSyntaxVisitor<>).MakeGenericType(ReturnType);
        var instance = Activator.CreateInstance(activator);

        if (instance == null)
            throw new InvalidOperationException($"failed to create the instance of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.");

        var register = activator.GetMethod(nameof(RootCSharpSyntaxVisitor<SyntaxNode>.AddVisitor), BindingFlags.Instance | BindingFlags.Public);
        if (register == null)
            throw new InvalidOperationException($"failed to create the invoker of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.Register(CSharpSyntaxVisitor<{ReturnType.FullName}>)");

        var visit1 = activator.GetMethod(nameof(RootCSharpSyntaxVisitor<SyntaxNode>.Visit), BindingFlags.Instance | BindingFlags.Public, new[] { typeof(Microsoft.CodeAnalysis.SyntaxNode) });
        if (visit1 == null)
            throw new InvalidOperationException($"failed to create the invoker of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.Visit(Microsoft.CodeAnalysis.SyntaxNode)");

        var visit2 = activator.GetMethod(nameof(RootCSharpSyntaxVisitor<SyntaxNode>.Visit), BindingFlags.Instance | BindingFlags.Public, new[] { typeof(Microsoft.CodeAnalysis.SyntaxNode), ReturnType });
        if (visit2 == null)
            throw new InvalidOperationException($"failed to create the invoker of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.Visit(Microsoft.CodeAnalysis.SyntaxNode)");
        return (instance, register, visit1, visit2);
    }

    private object CreateBackendVisitorArgs(SemanticModel semanticModel, Expression<Func<Microsoft.CodeAnalysis.SyntaxNode, SyntaxNode?>> visit1, Expression<Func<Microsoft.CodeAnalysis.SyntaxNode, SyntaxNode?, SyntaxNode?>> visit2,
                                            Expression<Func<string, Microsoft.CodeAnalysis.SyntaxNode?, SyntaxNode?>>? invoker = default)
    {
        var activator = typeof(BackendVisitorArgs<>).MakeGenericType(ReturnType);
        var instance = Activator.CreateInstance(activator, semanticModel, CreateDelegate1Lambda(visit1), CreateDelegate2Lambda(visit2), CreateInvokerLambda(invoker));

        if (instance == null)
            throw new InvalidOperationException($"failed to create the instance of BackendVisitorArgs<{ReturnType.FullName}>.");

        return instance;
    }

    private Delegate CreateDelegate1Lambda(Expression<Func<Microsoft.CodeAnalysis.SyntaxNode, SyntaxNode?>> visit)
    {
        var parameter = Expression.Parameter(typeof(Microsoft.CodeAnalysis.SyntaxNode));
        var invocation = Expression.Invoke(visit, parameter);
        var cast = Expression.Convert(invocation, ReturnType);
        var lambda = Expression.Lambda(cast, parameter);
        var @delegate = lambda.Compile();

        return @delegate;
    }

    private Delegate CreateDelegate2Lambda(Expression<Func<Microsoft.CodeAnalysis.SyntaxNode, SyntaxNode?, SyntaxNode?>> visit)
    {
        var parameter1 = Expression.Parameter(typeof(Microsoft.CodeAnalysis.SyntaxNode));
        var parameter2 = Expression.Parameter(typeof(SyntaxNode));
        var invocation = Expression.Invoke(visit, parameter1, parameter2);
        var cast = Expression.Convert(invocation, ReturnType);
        var lambda = Expression.Lambda(cast, parameter1, parameter2);
        var @delegate = lambda.Compile();

        return @delegate;
    }

    private Delegate? CreateInvokerLambda(Expression<Func<string, Microsoft.CodeAnalysis.SyntaxNode?, SyntaxNode?>>? invoker)
    {
        if (invoker == null)
            return null;

        var parameter1 = Expression.Parameter(typeof(string));
        var parameter2 = Expression.Parameter(typeof(Microsoft.CodeAnalysis.SyntaxNode));
        var invocation = Expression.Invoke(invoker, parameter1, parameter2);
        var lambda = Expression.Lambda(invocation, parameter1, parameter2);
        var @delegate = lambda.Compile();

        return @delegate;
    }
}