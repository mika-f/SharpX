// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class QualifiedNameSyntaxInternal : NameSyntaxInternal
{
    public NameSyntaxInternal Left { get; }

    public SyntaxTokenInternal DotToken { get; }

    public SimpleNameSyntaxInternal Right { get; }

    public QualifiedNameSyntaxInternal(SyntaxKind kind, NameSyntaxInternal left, SyntaxTokenInternal dotToken, SimpleNameSyntaxInternal right) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(left);
        Left = left;

        AdjustWidth(dotToken);
        DotToken = dotToken;

        AdjustWidth(right);
        Right = right;
    }

    public QualifiedNameSyntaxInternal(SyntaxKind kind, NameSyntaxInternal left, SyntaxTokenInternal dotToken, SimpleNameSyntaxInternal right, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(left);
        Left = left;

        AdjustWidth(dotToken);
        DotToken = dotToken;

        AdjustWidth(right);
        Right = right;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new QualifiedNameSyntaxInternal(Kind, Left, DotToken, Right, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Left,
            1 => DotToken,
            2 => Right,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new QualifiedNameSyntax(this, parent, position);
    }
}