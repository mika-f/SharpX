// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Core;

namespace SharpX.Compiler.Models.Abstractions;

internal class BackendRegistry : IBackendRegistry
{
    private readonly List<BackendContainer> _containers;

    public BackendRegistry()
    {
        _containers = new List<BackendContainer>();
    }

    public void RegisterBackendVisitor(string language, Type visitor, Type @return, uint priority)
    {
        if (!IsAssignableToGenericType(visitor, typeof(CompositeCSharpSyntaxVisitor<>), typeof(SyntaxNode)))
            throw new ArgumentException("visitor must be inherit from CompositeCSharpSyntaxVisitor<T> and T is must be inherit from SharpX.Core.SyntaxNode", nameof(visitor));

        if (!@return.IsAssignableTo(typeof(SyntaxNode)))
            throw new ArgumentException("return must be inherit from SharpX.Core.SyntaxNode", nameof(@return));

        if (_containers.Any(w => w.Language == language))
        {
            var container = _containers.First(w => w.Language == language);
            if (container.ReturnType != @return)
                throw new ArgumentException("the return type must be same across language backend implementations", nameof(@return));

            container.Register(priority, visitor);
        }
        else
        {
            var container = new BackendContainer(language, @return);
            container.Register(priority, visitor);

            _containers.Add(container);
        }
    }

    public void RegisterReferences(string language, params string[] references)
    {
        var container = _containers.FirstOrDefault(w => w.Language == language);
        if (container == null)
            throw new InvalidOperationException();

        container.AddReferences(references);
    }

    public BackendContainer? GetLanguageContainer(string language)
    {
        return _containers.FirstOrDefault(w => string.Equals(w.Language, language, StringComparison.InvariantCultureIgnoreCase));
    }

    private static bool IsAssignableToGenericType(Type given, Type generics, params Type[] constraints)
    {
        if (given.IsGenericType)
        {
            var def = given.GetGenericTypeDefinition();
            if (def == generics)
            {
                var arguments = given.GenericTypeArguments;
                if (constraints.Length == 0)
                    return true; // skip arguments check if constraints is empty

                if (arguments.Length != constraints.Length)
                    return false;

                for (var i = 0; i < arguments.Length; i++)
                {
                    if (arguments[i].IsAssignableTo(constraints[i]))
                        continue;
                    return false;
                }

                return true;
            }
        }


        var @base = given.BaseType;
        if (@base == null)
            return false;

        return IsAssignableToGenericType(@base, generics, constraints);
    }
}