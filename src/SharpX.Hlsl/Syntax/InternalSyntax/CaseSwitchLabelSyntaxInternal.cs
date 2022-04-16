// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class CaseSwitchLabelSyntaxInternal : SwitchLabelSyntaxInternal
{
    public SyntaxTokenInternal CaseKeyword { get; }

    public ExpressionSyntaxInternal Value { get; }

    public SyntaxTokenInternal ColonToken { get; }

    public CaseSwitchLabelSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal caseKeyword, ExpressionSyntaxInternal value, SyntaxTokenInternal colonToken) : base(kind)
    {
        SlotCount = 3;

        AdjustWidth(caseKeyword);
        CaseKeyword = caseKeyword;

        AdjustWidth(value);
        Value = value;

        AdjustWidth(colonToken);
        ColonToken = colonToken;
    }

    public CaseSwitchLabelSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal caseKeyword, ExpressionSyntaxInternal value, SyntaxTokenInternal colonToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 3;

        AdjustWidth(caseKeyword);
        CaseKeyword = caseKeyword;

        AdjustWidth(value);
        Value = value;

        AdjustWidth(colonToken);
        ColonToken = colonToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CaseSwitchLabelSyntaxInternal(Kind, CaseKeyword, Value, ColonToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => CaseKeyword,
            1 => Value,
            2 => ColonToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        throw new NotImplementedException();
    }
}