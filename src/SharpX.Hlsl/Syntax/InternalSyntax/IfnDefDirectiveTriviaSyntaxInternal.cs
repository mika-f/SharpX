// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class IfnDefDirectiveTriviaSyntaxInternal : ConditionalDirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal IfnDefKeyword { get; }

    public override ExpressionSyntaxInternal Condition { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public IfnDefDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal ifnDefKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 4;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(ifnDefKeyword);
        IfnDefKeyword = ifnDefKeyword;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public IfnDefDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal ifnDefKeyword, ExpressionSyntaxInternal condition, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) :
        base(kind, diagnostics, annotations)
    {
        SlotCount = 4;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(ifnDefKeyword);
        IfnDefKeyword = ifnDefKeyword;

        AdjustWidth(condition);
        Condition = condition;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new IfnDefDirectiveTriviaSyntaxInternal(Kind, HashToken, IfnDefKeyword, Condition, EndOfDirectiveToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new IfnDefDirectiveTriviaSyntaxInternal(Kind, HashToken, IfnDefKeyword, Condition, EndOfDirectiveToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => IfnDefKeyword,
            2 => Condition,
            3 => EndOfDirectiveToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        throw new NotImplementedException();
    }
}