// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class BlockSyntaxInternal : StatementSyntaxInternal
{
    private readonly GreenNode? _attributeLists;
    private readonly GreenNode? _statements;

    public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public SyntaxTokenInternal OpenBraceToken { get; }

    public SyntaxListInternal<StatementSyntaxInternal> Statements => new(_statements);

    public SyntaxTokenInternal CloseBraceToken { get; }


    public BlockSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal openBraceToken, GreenNode? statements, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 4;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (statements != null)
        {
            AdjustWidth(statements);
            _statements = statements;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public BlockSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal openBraceToken, GreenNode? statements, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 4;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (statements != null)
        {
            AdjustWidth(statements);
            _statements = statements;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new BlockSyntaxInternal(Kind, _attributeLists, OpenBraceToken, _statements, CloseBraceToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new BlockSyntaxInternal(Kind, _attributeLists, OpenBraceToken, _statements, CloseBraceToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => OpenBraceToken,
            2 => _statements,
            3 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new BlockSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitBlock(this);
    }
}