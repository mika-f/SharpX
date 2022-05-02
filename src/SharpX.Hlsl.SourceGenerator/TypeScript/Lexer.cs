// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace SharpX.Hlsl.SourceGenerator.TypeScript;

internal static class Lexer
{
    public static Queue<Token> Tokenize(string source)
    {
        var tokens = new Queue<Token>();

        source = source.Trim();
        source = Regex.Replace(source, "/\\*[\\s\\S]*?\\*/", "", RegexOptions.Multiline);
        source = Regex.Replace(source, "//.*$", "", RegexOptions.Multiline);

        Tokenize(source.AsSpan(), tokens);

        return tokens;
    }

    private static void Tokenize(ReadOnlySpan<char> source, Queue<Token> tokens)
    {
        while (source.Length > 0)
            switch (source[0])
            {
                case ' ':
                case '\t':
                case '\r':
                case '\n':
                    source = source.Slice(1);
                    break;

                case '&':
                    tokens.Enqueue(new Token(SyntaxKind.AmpersandToken, "&"));
                    source = source.Slice(1);
                    break;

                case '(':
                    tokens.Enqueue(new Token(SyntaxKind.OpenParenToken, "("));
                    source = source.Slice(1);
                    break;

                case ')':
                    tokens.Enqueue(new Token(SyntaxKind.CloseParenToken, ")"));
                    source = source.Slice(1);
                    break;

                case '=':
                    tokens.Enqueue(new Token(SyntaxKind.EqualsToken, "="));
                    source = source.Slice(1);
                    break;

                case '[':
                    tokens.Enqueue(new Token(SyntaxKind.OpenBraceToken, "["));
                    source = source.Slice(1);
                    break;

                case ']':
                    tokens.Enqueue(new Token(SyntaxKind.CloseBraceToken, "]"));
                    source = source.Slice(1);
                    break;

                case '{':
                    tokens.Enqueue(new Token(SyntaxKind.OpenBracketToken, "{"));
                    source = source.Slice(1);
                    break;

                case '}':
                    tokens.Enqueue(new Token(SyntaxKind.CloseBracketToken, "}"));
                    source = source.Slice(1);
                    break;

                case '|':
                    tokens.Enqueue(new Token(SyntaxKind.BarToken, "|"));
                    source = source.Slice(1);
                    break;

                case ':':
                    tokens.Enqueue(new Token(SyntaxKind.ColonToken, ":"));
                    source = source.Slice(1);
                    break;

                case ';':
                    tokens.Enqueue(new Token(SyntaxKind.SemicolonToken, ";"));
                    source = source.Slice(1);
                    break;

                case '<':
                    tokens.Enqueue(new Token(SyntaxKind.LessThanToken, "<"));
                    source = source.Slice(1);
                    break;

                case ',':
                    tokens.Enqueue(new Token(SyntaxKind.CommaToken, ","));
                    source = source.Slice(1);
                    break;

                case '>':
                    tokens.Enqueue(new Token(SyntaxKind.GreaterThanToken, ">"));
                    source = source.Slice(1);
                    break;

                case { } when char.IsDigit(source[0]):
                {
                    var offset = 1;
                    while (source.Length > 0 && char.IsLetterOrDigit(source[offset]))
                        offset++;

                    tokens.Enqueue(new Token(SyntaxKind.Numeric, source.Slice(0, offset).ToString()));
                    source = source.Slice(offset);
                    break;
                }

                case { } when char.IsLetter(source[0]):
                {
                    var offset = 1;
                    while (source.Length > 0 && (char.IsLetterOrDigit(source[offset]) || source[offset] == '_'))
                        offset++;

                    var str = source.Slice(0, offset).ToString();
                    switch (str)
                    {
                        case "any":
                            tokens.Enqueue(new Token(SyntaxKind.AnyKeyword, "any"));
                            break;

                        case "extends":
                            tokens.Enqueue(new Token(SyntaxKind.ExtendsKeyword, "extends"));
                            break;

                        case "export":
                            tokens.Enqueue(new Token(SyntaxKind.ExportKeyword, "export"));
                            break;

                        case "type":
                            tokens.Enqueue(new Token(SyntaxKind.TypeKeyword, "type"));
                            break;

                        default:
                            tokens.Enqueue(new Token(SyntaxKind.Identifier, source.Slice(0, offset).ToString()));
                            break;
                    }

                    source = source.Slice(offset);
                    break;
                }

                default:
                    Debug.WriteLine($"the token {source[0]} is skipped because it is not supported");
                    source = source.Slice(1);
                    break;
            }
    }
}