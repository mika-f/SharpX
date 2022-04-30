// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Reflection;

using SharpX.Composition.Interfaces;
using SharpX.Core;

namespace SharpX.Compiler.Models.Abstractions;

internal class BackendContainer
{
    private readonly SortedList<uint, Type> _visitors;

    public string Language { get; }

    public Type ReturnType { get; }

    public BackendContainer(string language, Type @return)
    {
        Language = language;
        ReturnType = @return;
        _visitors = new SortedList<uint, Type>();
    }

    public void Register(uint priority, Type visitor)
    {
        _visitors.Add(priority, visitor);
    }

    public SyntaxNode? RunAsync(Microsoft.CodeAnalysis.SyntaxNode syntax, IBackendVisitorArgs args)
    {
        var activator = typeof(RootCSharpSyntaxVisitor<>).MakeGenericType(ReturnType);
        var instance = Activator.CreateInstance(activator);

        if (instance == null)
            throw new InvalidOperationException($"failed to create the instance of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.");

        var register = activator.GetMethod(nameof(RootCSharpSyntaxVisitor<SyntaxNode>.AddVisitor), BindingFlags.Instance | BindingFlags.Public);
        if (register == null)
            throw new InvalidOperationException($"failed to create the invoker of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.Register(CSharpSyntaxVisitor<{ReturnType.FullName}>)");

        foreach (var visitor in _visitors)
        {
            var visitorInstance = Activator.CreateInstance(visitor.Value, args);
            register.Invoke(instance, new[] { visitorInstance });
        }

        var visit = activator.GetMethod(nameof(RootCSharpSyntaxVisitor<SyntaxNode>.Visit), BindingFlags.Instance | BindingFlags.Public);
        if (visit == null)
            throw new InvalidOperationException($"failed to create the invoker of RootCSharpSyntaxVisitor<{ReturnType.FullName}>.Visit(Microsoft.CodeAnalysis.SyntaxNode)");

        return visit.Invoke(instance, new object?[] { syntax }) as SyntaxNode;
    }
}