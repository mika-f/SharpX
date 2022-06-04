// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Core;

using AttributeListSyntax = SharpX.Hlsl.Syntax.AttributeListSyntax;
using ExpressionStatementSyntax = SharpX.Hlsl.Syntax.ExpressionStatementSyntax;
using ExpressionSyntax = SharpX.Hlsl.Syntax.ExpressionSyntax;
using ReturnStatementSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.ReturnStatementSyntax;
using StatementSyntax = SharpX.Hlsl.Syntax.StatementSyntax;
using TypeSyntax = SharpX.Hlsl.Syntax.TypeSyntax;

namespace SharpX.Hlsl.CSharp.ObjectInitializer;

internal class HlslNodeVisitor : CompositeCSharpSyntaxVisitor<HlslSyntaxNode>
{
    private static readonly SyntaxList<AttributeListSyntax> EmptyAttributes = SyntaxFactory.List<AttributeListSyntax>();
    private readonly IBackendVisitorArgs<HlslSyntaxNode> _args;
    private readonly Stack<string> _scope;
    private readonly Stack<List<StatementSyntax>> _statements;

    public HlslNodeVisitor(IBackendVisitorArgs<HlslSyntaxNode> args) : base(args)
    {
        _args = args;
        _statements = new Stack<List<StatementSyntax>>();
        _scope = new Stack<string>();
    }

    public override HlslSyntaxNode? VisitReturnStatement(ReturnStatementSyntax oldNode, HlslSyntaxNode? newNode)
    {
        // if return statement's expression is object creation syntax, rewritten into block syntax with object assignments.
        if (oldNode.Expression is ObjectCreationExpressionSyntax { Initializer: { } })
            return Visit(oldNode.Expression);

        return newNode;
    }

    public override HlslSyntaxNode? VisitObjectCreationExpression(ObjectCreationExpressionSyntax node)
    {
        const string variableName = "__initializer__";

        // converted initializer is SCOPED by block, because initializer's variable is fixed.
        var t = (TypeSyntax)_args.Invoke("HLSL", node.Type)!;
        var cast = SyntaxFactory.CastExpression(t, SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(0)));
        var initializer = SyntaxFactory.EqualsValueClause(cast);
        var variable = SyntaxFactory.VariableDeclarator(SyntaxFactory.Identifier(variableName), initializer);
        var declaration = SyntaxFactory.VariableDeclaration(cast.Type, SyntaxFactory.SeparatedList(variable));
        var block = SyntaxFactory.Block(SyntaxFactory.LocalDeclaration(EmptyAttributes, SyntaxFactory.TokenList(), declaration));

        _scope.Push(variableName);

        foreach (var expression in node.Initializer!.Expressions)
            block = block.AddStatements((ExpressionStatementSyntax)Visit(expression)!);

        _scope.Pop();

        var @return = SyntaxFactory.ReturnStatement(EmptyAttributes, SyntaxFactory.IdentifierName(variableName));
        return block.AddStatements(@return);
    }

    public override HlslSyntaxNode? VisitAssignmentExpression(AssignmentExpressionSyntax node)
    {
        if (_scope.TryPeek(out var scope))
        {
            var identifier = SyntaxFactory.IdentifierName(scope);
            var left = SyntaxFactory.MemberAccessExpression(identifier, SyntaxFactory.IdentifierName(node.Left.ToFullString().Trim()));
            var right = _args.Invoke("HLSL", node.Right);
            if (right is not ExpressionSyntax expression)
                return null;
            var assignment = SyntaxFactory.AssignmentExpression(SyntaxKind.SimpleAssignmentExpression, left, expression);

            return SyntaxFactory.ExpressionStatement(EmptyAttributes, assignment);
        }

        return null;
    }
}