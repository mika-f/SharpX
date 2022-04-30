// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Core.Extensions;

public static class GreenNodeExtensions
{
    public static SyntaxListInternal<T> ToGreenList<T>(this SyntaxNode? node) where T : GreenNode
    {
        return node != null ? ToGreenList<T>(node.Green) : default;
    }

    public static SeparatedSyntaxListInternal<T> ToGreenSeparatedList<T>(this SyntaxNode? node) where T : GreenNode
    {
        return node != null ? new SeparatedSyntaxListInternal<T>(ToGreenList<T>(node.Green)) : default;
    }

    public static SyntaxListInternal<T> ToGreenList<T>(this GreenNode? node) where T : GreenNode
    {
        return new SyntaxListInternal<T>(node);
    }

    public static TNode WithAnnotationsGreen<TNode>(this TNode node, IEnumerable<SyntaxAnnotation> annotations) where TNode : GreenNode
    {
        var newAnnotations = annotations.Distinct().ToArray();
        if (newAnnotations.Any()) return (TNode)node.SetAnnotations(newAnnotations);

        var existingAnnotations = node.GetAnnotations();
        if (existingAnnotations.Length == 0)
            return node;
        return (TNode)node.SetAnnotations(null);
    }
}