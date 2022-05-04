// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class DiscardStatementSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal DiscardKeyword { get; }

    public SyntaxTokenInternal SemicolonToken { get; }


    public DiscardStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal discardKeyword, SyntaxTokenInternal semicolonToken) : base(kind)
    {
        SlotCount = 3;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(discardKeyword);
        DiscardKeyword = discardKeyword;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public DiscardStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal discardKeyword, SyntaxTokenInternal semicolonToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 3;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(discardKeyword);
        DiscardKeyword = discardKeyword;

        AdjustWidth(semicolonToken);
        SemicolonToken = semicolonToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new BreakStatementSyntaxInternal(Kind, _attributeLists, DiscardKeyword, SemicolonToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new BreakStatementSyntaxInternal(Kind, _attributeLists, DiscardKeyword, SemicolonToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => DiscardKeyword,
            2 => SemicolonToken,
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