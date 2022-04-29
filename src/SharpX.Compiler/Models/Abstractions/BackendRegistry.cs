// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis.CSharp;

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

    public BackendContainer? GetLanguageContainer(string language)
    {
        return _containers.FirstOrDefault(w => w.Language == language);
    }

    public void RegisterBackendVisitor(string language, Type visitor, uint priority)
    {
        if (!IsAssignableToGenericType(visitor, typeof(CSharpSyntaxVisitor<>), typeof(SyntaxNode)))
            throw new ArgumentException("visitor must be inherit from CSharpSyntaxVisitor<T> and T is must be inherit from SharpX.Core.SyntaxNode", nameof(visitor));

        if (_containers.Any(w => w.Language == language))
        {
            var container = _containers.First(w => w.Language == language);
            container.Register(priority, visitor);
        }
        else
        {
            var container = new BackendContainer(language);
            container.Register(priority, visitor);

            _containers.Add(container);
        }
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