// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.ShaderLab.Syntax;

internal static class SyntaxFacts
{
    public static string GetText(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.OpenParenToken => "(",
            SyntaxKind.CloseParenToken => ")",
            SyntaxKind.PlusToken => "+",
            SyntaxKind.MinusToken => "-",
            SyntaxKind.EqualsToken => "=",
            SyntaxKind.OpenBraceToken => "{",
            SyntaxKind.CloseBraceToken => "}",
            SyntaxKind.OpenBracketToken => "[",
            SyntaxKind.CloseBracketToken => "]",
            SyntaxKind.CommaToken => ",",
            SyntaxKind.DoubleQuoteToken => "\"",
            SyntaxKind.DotToken => ".",
            SyntaxKind.ColorKeyword => "Color",
            SyntaxKind.CustomEditorKeyword => "CustomEditor",
            SyntaxKind.FallbackKeyword => "Fallback",
            SyntaxKind.OffKeyword => "Off",
            SyntaxKind.PassKeyword => "Pass",
            SyntaxKind.PropertiesKeyword => "Properties",
            SyntaxKind.ShaderKeyword => "Shader",
            SyntaxKind.SubShaderKeyword => "SubShader"
        };
    }
}