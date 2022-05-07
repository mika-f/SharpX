// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

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

    public Func<SyntaxNode, string> ExtensionCallback { get; private set; }

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

    public void AddExtensionCallback(Func<SyntaxNode, string> callback)
    {
        ExtensionCallback = callback;
    }

    public SyntaxNode? RunAsync(Microsoft.CodeAnalysis.SyntaxNode syntax, SemanticModel model)
    {
        var (instance, register, visit) = CreateRootSyntaxVisitorInstance();
        var args = CreateBackendVisitorArgs(model, node => (SyntaxNode?)visit.Invoke(instance, new object?[] { node }));

        foreach (var visitor in _visitors)
        {
            var visitorInstance = Activator.CreateInstance(visitor.Value, args);
            register.Invoke(instance, new[] { visitorInstance });
        }


        return visit.Invoke(instance, new object?[] { syntax }) as SyntaxNode;
    }

    private (object, MethodInfo, MethodInfo) CreateRootSyntaxVisitorInstance()
    {
        var activator = typeof(RootCSharpSyntaxVisitor<>).MakeGenericType(ReturnType);
        var instance = Activator.CreateInstance(activator);

        if (instance == null)
            throw new InvalidOperationException($"failed to create the instance of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.");

        var register = activator.GetMethod(nameof(RootCSharpSyntaxVisitor<SyntaxNode>.AddVisitor), BindingFlags.Instance | BindingFlags.Public);
        if (register == null)
            throw new InvalidOperationException($"failed to create the invoker of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.Register(CSharpSyntaxVisitor<{ReturnType.FullName}>)");

        var visit = activator.GetMethod(nameof(RootCSharpSyntaxVisitor<SyntaxNode>.Visit), BindingFlags.Instance | BindingFlags.Public);
        if (visit == null)
            throw new InvalidOperationException($"failed to create the invoker of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.Visit(Microsoft.CodeAnalysis.SyntaxNode)");

        return (instance, register, visit);
    }

    private object CreateBackendVisitorArgs(SemanticModel semanticModel, Expression<Func<Microsoft.CodeAnalysis.SyntaxNode, SyntaxNode?>> visit)
    {
        var parameter = Expression.Parameter(typeof(Microsoft.CodeAnalysis.SyntaxNode));
        var invocation = Expression.Invoke(visit, parameter);
        var cast = Expression.Convert(invocation, ReturnType);
        var lambda = Expression.Lambda(cast, parameter);
        var @delegate = lambda.Compile();

        var activator = typeof(BackendVisitorArgs<>).MakeGenericType(ReturnType);
        var instance = Activator.CreateInstance(activator, semanticModel, @delegate);

        if (instance == null)
            throw new InvalidOperationException($"failed to create the instance of BackendVisitorArgs<{ReturnType.FullName}>.");

        return instance;
    }
}