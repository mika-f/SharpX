// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class PragmaDefDirectiveTriviaSyntaxInternal : DirectiveTriviaSyntaxInternal
{
    public override SyntaxTokenInternal HashToken { get; }

    public SyntaxTokenInternal PragmaKeyword { get; }

    public SyntaxTokenInternal DefKeyword { get; }

    public SyntaxTokenInternal OpenParenToken { get; }

    public SyntaxTokenInternal Target { get; }

    public SyntaxTokenInternal FirstCommaToken { get; }

    public SyntaxTokenInternal Register { get; }

    public SyntaxTokenInternal SecondCommaToken { get; }

    public SyntaxTokenInternal Val1 { get; }

    public SyntaxTokenInternal ThirdCommaToken { get; }

    public SyntaxTokenInternal Val2 { get; }

    public SyntaxTokenInternal FourthCommaToken { get; }

    public SyntaxTokenInternal Val3 { get; }

    public SyntaxTokenInternal FifthCommaToken { get; }

    public SyntaxTokenInternal Val4 { get; }

    public SyntaxTokenInternal CloseParenToken { get; }

    public override SyntaxTokenInternal EndOfDirectiveToken { get; }

    public PragmaDefDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal defKeyword, SyntaxTokenInternal openParenToken, SyntaxTokenInternal target, SyntaxTokenInternal firstCommaToken, SyntaxTokenInternal register,
                                                  SyntaxTokenInternal secondCommaToken, SyntaxTokenInternal val1, SyntaxTokenInternal thirdCommaToken, SyntaxTokenInternal val2, SyntaxTokenInternal fourthCommaToken, SyntaxTokenInternal val3, SyntaxTokenInternal fifthCommaToken,
                                                  SyntaxTokenInternal val4,
                                                  SyntaxTokenInternal closeParenToken, SyntaxTokenInternal endOfDirectiveToken) : base(kind)
    {
        SlotCount = 17;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(defKeyword);
        DefKeyword = defKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(target);
        Target = target;

        AdjustWidth(firstCommaToken);
        FirstCommaToken = firstCommaToken;

        AdjustWidth(register);
        Register = register;

        AdjustWidth(secondCommaToken);
        SecondCommaToken = secondCommaToken;

        AdjustWidth(val1);
        Val1 = val1;

        AdjustWidth(thirdCommaToken);
        ThirdCommaToken = thirdCommaToken;

        AdjustWidth(val2);
        Val2 = val2;

        AdjustWidth(fourthCommaToken);
        FourthCommaToken = fourthCommaToken;

        AdjustWidth(val3);
        Val3 = val3;

        AdjustWidth(fifthCommaToken);
        FifthCommaToken = fifthCommaToken;

        AdjustWidth(val4);
        Val4 = val4;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public PragmaDefDirectiveTriviaSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal hashToken, SyntaxTokenInternal pragmaKeyword, SyntaxTokenInternal defKeyword, SyntaxTokenInternal openParenToken, SyntaxTokenInternal target, SyntaxTokenInternal firstCommaToken, SyntaxTokenInternal register,
                                                  SyntaxTokenInternal secondCommaToken, SyntaxTokenInternal val1, SyntaxTokenInternal thirdCommaToken, SyntaxTokenInternal val2, SyntaxTokenInternal fourthCommaToken, SyntaxTokenInternal val3, SyntaxTokenInternal fifthCommaToken,
                                                  SyntaxTokenInternal val4, SyntaxTokenInternal closeParenToken, SyntaxTokenInternal endOfDirectiveToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 17;

        AdjustWidth(hashToken);
        HashToken = hashToken;

        AdjustWidth(pragmaKeyword);
        PragmaKeyword = pragmaKeyword;

        AdjustWidth(defKeyword);
        DefKeyword = defKeyword;

        AdjustWidth(openParenToken);
        OpenParenToken = openParenToken;

        AdjustWidth(target);
        Target = target;

        AdjustWidth(firstCommaToken);
        FirstCommaToken = firstCommaToken;

        AdjustWidth(register);
        Register = register;

        AdjustWidth(secondCommaToken);
        SecondCommaToken = secondCommaToken;

        AdjustWidth(val1);
        Val1 = val1;

        AdjustWidth(thirdCommaToken);
        ThirdCommaToken = thirdCommaToken;

        AdjustWidth(val2);
        Val2 = val2;

        AdjustWidth(fourthCommaToken);
        FourthCommaToken = fourthCommaToken;

        AdjustWidth(val3);
        Val3 = val3;

        AdjustWidth(fifthCommaToken);
        FifthCommaToken = fifthCommaToken;

        AdjustWidth(val4);
        Val4 = val4;

        AdjustWidth(closeParenToken);
        CloseParenToken = closeParenToken;

        AdjustWidth(endOfDirectiveToken);
        EndOfDirectiveToken = endOfDirectiveToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new PragmaDefDirectiveTriviaSyntaxInternal(
            Kind,
            HashToken,
            PragmaKeyword,
            DefKeyword,
            OpenParenToken,
            Target,
            FirstCommaToken,
            Register,
            SecondCommaToken,
            Val1,
            ThirdCommaToken,
            Val2,
            FourthCommaToken,
            Val3,
            FifthCommaToken,
            Val4,
            CloseParenToken,
            EndOfDirectiveToken,
            diagnostics
        );
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => HashToken,
            1 => PragmaKeyword,
            2 => DefKeyword,
            3 => OpenParenToken,
            4 => Target,
            5 => FirstCommaToken,
            6 => Register,
            7 => SecondCommaToken,
            8 => Val1,
            9 => ThirdCommaToken,
            10 => Val2,
            11 => FourthCommaToken,
            12 => Val3,
            13 => FifthCommaToken,
            14 => Val4,
            15 => CloseParenToken,
            16 => EndOfDirectiveToken,
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