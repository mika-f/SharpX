// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;

using SharpX.Hlsl.SourceGenerator.TypeScript.Syntax;

namespace SharpX.Hlsl.SourceGenerator.TypeScript;

/**
 * simple d.ts parser for HLSL source generator
 */
internal sealed class Parser
{
    public static CompilationUnitSyntax ParseString(string source)
    {
        var tokens = Lexer.Tokenize(source);
        return ParseCompilationUnit(tokens);
    }

    private static CompilationUnitSyntax ParseCompilationUnit(Queue<Token> tokens)
    {
        var members = new List<MemberDeclarationSyntax>();

        while (tokens.Count > 0)
            members.Add(ParseMemberDeclaration(tokens));

        return new CompilationUnitSyntax(members);
    }

    private static MemberDeclarationSyntax ParseMemberDeclaration(Queue<Token> tokens)
    {
        MemberDeclarationSyntax val = tokens.Peek().Kind switch
        {
            SyntaxKind.TypeKeyword => ParseTypeDeclaration(tokens),
            SyntaxKind.ExportKeyword => ParseExportStatement(tokens),
            _ => throw new ArgumentOutOfRangeException(nameof(tokens), $"SharpX TypeScript parser does not support declaration of type {tokens.Peek().Kind} in currently")
        };

        if (tokens.Peek().Kind == SyntaxKind.SemicolonToken)
            tokens.Dequeue();

        return val;
    }

    private static TypeDeclarationSyntax ParseTypeDeclaration(Queue<Token> tokens)
    {
        tokens.Dequeue(); // type keyword

        var t = ParseTypeSyntax(tokens);

        if (tokens.Peek().Kind != SyntaxKind.EqualsToken)
            return new TypeDeclarationSyntax(t, new List<FunctionDeclarationSyntax>());

        tokens.Dequeue(); // =

        if (tokens.Peek().Kind == SyntaxKind.Identifier)
        {
            var aliases = new List<TypeSyntax>();

            while (tokens.Count > 0)
            {
                var a = ParseTypeSyntax(tokens);
                aliases.Add(a);

                if (tokens.Peek().Kind == SyntaxKind.BarToken)
                    tokens.Dequeue();
                else
                    break;
            }

            return new TypeAliasSyntax(t, aliases);
        }

        var functions = ParseTypeDeclarationBody(tokens);

        return new TypeDeclarationSyntax(t, functions);
    }

    private static TypeSyntax ParseTypeSyntax(Queue<Token> tokens, bool allowNumeric = false)
    {
        var it = tokens.Dequeue();
        if (it.Kind != SyntaxKind.Identifier && it.Kind != SyntaxKind.AnyKeyword)
            if (allowNumeric && it.Kind != SyntaxKind.Numeric)
                throw new ArgumentException("type declaration must be type [IDENTIFIER] ...");
            else if (!allowNumeric)
                throw new ArgumentException("type declaration must be type [IDENTIFIER] ...");

        var identifier = new IdentifierSyntax(it);

        if (it.Kind == SyntaxKind.AnyKeyword)
            return new SimpleTypeSyntax(identifier);

        if (tokens.Peek().Kind == SyntaxKind.LessThanToken)
        {
            var generics = ParseGenericsDeclaration(tokens);
            return new GenericTypeSyntax(identifier, generics);
        }

        return new SimpleTypeSyntax(identifier);
    }

    private static GenericsDeclarationSyntax ParseGenericsDeclaration(Queue<Token> tokens)
    {
        tokens.Dequeue(); // <

        var generics = new List<GenericsSyntax>();

        while (tokens.Count > 0)
        {
            var p1 = tokens.Peek();
            if (p1.Kind != SyntaxKind.Identifier && p1.Kind != SyntaxKind.Numeric)
                break;

            var t = ParseTypeSyntax(tokens, true);
            var generic = new GenericsSyntax(t, null);

            while (tokens.Count > 0)
            {
                var p2 = tokens.Peek();
                if (p2.Kind == SyntaxKind.ExtendsKeyword)
                {
                    generic = generic.WithGenericsConstraint(ParseConstraint(tokens));
                }
                else if (p2.Kind == SyntaxKind.BarToken)
                {
                    tokens.Dequeue(); // | 
                    generic = generic.AddOrTypes(ParseTypeSyntax(tokens));
                }
                else if (p2.Kind == SyntaxKind.CommaToken)
                {
                    tokens.Dequeue(); // ,
                    break;
                }
                else if (p2.Kind == SyntaxKind.GreaterThanToken)
                {
                    break;
                }
                else
                {
                    throw new ArgumentException(nameof(p2));
                }
            }

            generics.Add(generic);
        }

        tokens.Dequeue(); // >

        return new GenericsDeclarationSyntax(generics);
    }

    private static GenericsConstraintSyntax ParseConstraint(Queue<Token> tokens)
    {
        tokens.Dequeue(); // extends keyword

        var constraints = new List<TypeSyntax>();

        while (tokens.Count > 0)
        {
            var t = ParseTypeSyntax(tokens);

            if (tokens.Peek().Kind == SyntaxKind.EqualsToken)
            {
                tokens.Dequeue(); // = 

                t = new DefaultTypeSyntax(t, ParseTypeSyntax(tokens));
            }

            constraints.Add(t);

            if (tokens.Peek().Kind == SyntaxKind.CommaToken)
                break;

            if (tokens.Peek().Kind == SyntaxKind.GreaterThanToken)
                break;
        }

        return new GenericsConstraintSyntax(constraints);
    }

    private static List<FunctionDeclarationSyntax> ParseTypeDeclarationBody(Queue<Token> tokens)
    {
        var functions = new List<FunctionDeclarationSyntax>();

        tokens.Dequeue(); // {

        while (tokens.Count > 0)
        {
            if (tokens.Peek().Kind != SyntaxKind.Identifier && tokens.Peek().Kind != SyntaxKind.AnyKeyword)
                break;

            functions.Add(ParseFunctionDeclaration(tokens));

            if (tokens.Peek().Kind == SyntaxKind.CloseBraceToken)
                break;
        }


        tokens.Dequeue(); // }

        return functions;
    }

    private static FunctionDeclarationSyntax ParseFunctionDeclaration(Queue<Token> tokens)
    {
        var it = tokens.Dequeue(); // identifier
        if (it.Kind != SyntaxKind.Identifier && it.Kind != SyntaxKind.AnyKeyword)
            throw new ArgumentException(nameof(it));

        var identifier = new IdentifierSyntax(it);
        GenericsDeclarationSyntax? generics = null;

        if (tokens.Peek().Kind == SyntaxKind.LessThanToken)
            generics = ParseGenericsDeclaration(tokens);

        if (tokens.Peek().Kind != SyntaxKind.OpenParenToken)
            throw new ArgumentException(nameof(tokens));

        var parameters = new List<ParameterDeclarationSyntax>();

        tokens.Dequeue(); // (

        while (tokens.Count > 0)
        {
            if (tokens.Peek().Kind == SyntaxKind.CloseParenToken)
                break;

            var n = tokens.Dequeue();
            var name = new IdentifierSyntax(n);

            tokens.Dequeue(); // :

            var type = ParseTypeSyntax(tokens);

            parameters.Add(new ParameterDeclarationSyntax(name, type));

            if (tokens.Peek().Kind == SyntaxKind.CloseParenToken)
                break;

            tokens.Dequeue(); // ,
        }

        tokens.Dequeue(); // )
        tokens.Dequeue(); // :

        var r = ParseTypeSyntax(tokens);

        if (tokens.Peek().Kind == SyntaxKind.SemicolonToken)
            tokens.Dequeue(); // ;

        return new FunctionDeclarationSyntax(identifier, generics, parameters, r);
    }

    private static ExportStatementSyntax ParseExportStatement(Queue<Token> tokens)
    {
        tokens.Dequeue(); // export keyword

        var members = new List<MemberDeclarationSyntax>();

        switch (tokens.Peek().Kind)
        {
            case SyntaxKind.TypeKeyword:
                members.Add(ParseTypeDeclaration(tokens));
                break;

            default:
                throw new ArgumentOutOfRangeException(nameof(tokens), $"SharpX TypeScript parser does not support declaration of type {tokens.Peek().Kind} inside of exports statement in currently");
        }

        return new ExportStatementSyntax(members);
    }
}