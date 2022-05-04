// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class ArrayCreationExpressionSyntax : ExpressionSyntax
{
    private InitializerExpressionSyntax? _initializer;
    private ArrayTypeSyntax? _type;

    public ArrayTypeSyntax Type => GetRedAtZero(ref _type)!;

    public InitializerExpressionSyntax? Initializer => GetRed(ref _initializer, 1);

    internal ArrayCreationExpressionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _type)!,
            1 => GetRed(ref _initializer, 1)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _type,
            1 => _initializer,
            _ => null
        };
    }

    public ArrayCreationExpressionSyntax Update(ArrayTypeSyntax type, InitializerExpressionSyntax? initializer)
    {
        if (type != Type || initializer != Initializer)
            return SyntaxFactory.ArrayCreationExpression(type, initializer);
        return this;
    }

    public ArrayCreationExpressionSyntax WithArrayType(ArrayTypeSyntax type)
    {
        return Update(type, Initializer);
    }

    public ArrayCreationExpressionSyntax WithInitializer(InitializerExpressionSyntax? initializer)
    {
        return Update(Type, initializer);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitArrayCreationExpression(this);
    }
}