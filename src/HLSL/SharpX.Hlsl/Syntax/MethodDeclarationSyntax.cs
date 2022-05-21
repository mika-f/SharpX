// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class MethodDeclarationSyntax : MemberDeclarationSyntax
{
    private SyntaxNode? _attributeLists;
    private BlockSyntax? _body;
    private ParameterListSyntax? _parameterList;
    private SemanticSyntax? _returnSemantics;
    private TypeSyntax? _returnType;

    public SyntaxList<AttributeListSyntax> AttributeLists => new(GetRedAtZero(ref _attributeLists));

    public TypeSyntax ReturnType => GetRed(ref _returnType, 1)!;

    public SyntaxToken Identifier => new(this, ((MethodDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(2), GetChildIndex(2));

    public ParameterListSyntax ParameterList => GetRed(ref _parameterList, 3)!;

    public SemanticSyntax? ReturnSemantics => GetRed(ref _returnSemantics, 4);

    public BlockSyntax Body => GetRed(ref _body, 5)!;

    internal MethodDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _attributeLists),
            1 => GetRed(ref _returnType, 1),
            3 => GetRed(ref _parameterList, 3),
            4 => GetRed(ref _returnSemantics, 4),
            5 => GetRed(ref _body, 5),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => _returnType,
            3 => _parameterList,
            4 => _returnSemantics,
            5 => _body,
            _ => null
        };
    }

    public MethodDeclarationSyntax Update(SyntaxList<AttributeListSyntax> attributeLists, TypeSyntax returnType, SyntaxToken identifier, ParameterListSyntax parameterList, SemanticSyntax? returnSemantics, BlockSyntax body)
    {
        if (attributeLists != AttributeLists || returnType != ReturnType || identifier != Identifier || parameterList != ParameterList || returnSemantics != ReturnSemantics || body != Body)
            return SyntaxFactory.MethodDeclaration(attributeLists, returnType, identifier, parameterList, returnSemantics, body);
        return this;
    }

    public MethodDeclarationSyntax WithAttributeLists(SyntaxList<AttributeListSyntax> attributeLists)
    {
        return Update(attributeLists, ReturnType, Identifier, ParameterList, ReturnSemantics, Body);
    }

    public MethodDeclarationSyntax WithReturnType(TypeSyntax returnType)
    {
        return Update(AttributeLists, returnType, Identifier, ParameterList, ReturnSemantics, Body);
    }

    public MethodDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(AttributeLists, ReturnType, identifier, ParameterList, ReturnSemantics, Body);
    }

    public MethodDeclarationSyntax WithParameterList(ParameterListSyntax parameterList)
    {
        return Update(AttributeLists, ReturnType, Identifier, parameterList, ReturnSemantics, Body);
    }

    public MethodDeclarationSyntax WithReturnSemantics(SemanticSyntax returnSemantics)
    {
        return Update(AttributeLists, ReturnType, Identifier, ParameterList, returnSemantics, Body);
    }

    public MethodDeclarationSyntax WithBody(BlockSyntax body)
    {
        return Update(AttributeLists, ReturnType, Identifier, ParameterList, ReturnSemantics, body);
    }

    public MethodDeclarationSyntax AddAttributeLists(params AttributeListSyntax[] items)
    {
        return WithAttributeLists(AttributeLists.AddRange(items));
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitMethodDeclaration(this);
    }
}