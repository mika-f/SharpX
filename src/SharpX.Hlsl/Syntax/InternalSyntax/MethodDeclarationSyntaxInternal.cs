// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class MethodDeclarationSyntaxInternal : MemberDeclarationSyntaxInternal
{
    private readonly GreenNode? _attributeLists;

    public SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

    public TypeSyntaxInternal ReturnType { get; }

    public SyntaxTokenInternal Identifier { get; }

    public ParameterListSyntaxInternal ParameterList { get; }

    public SemanticSyntaxInternal? ReturnSemantic { get; }

    public BlockSyntaxInternal Body { get; }

    public MethodDeclarationSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, TypeSyntaxInternal returnType, SyntaxTokenInternal identifier, ParameterListSyntaxInternal parameterList, SemanticSyntaxInternal? returnSemantic, BlockSyntaxInternal body) : base(kind)
    {
        SlotCount = 6;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(returnType);
        ReturnType = returnType;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(parameterList);
        ParameterList = parameterList;

        if (returnSemantic != null)
        {
            AdjustWidth(returnSemantic);
            ReturnSemantic = returnSemantic;
        }

        AdjustWidth(body);
        Body = body;
    }

    public MethodDeclarationSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, TypeSyntaxInternal returnType, SyntaxTokenInternal identifier, ParameterListSyntaxInternal parameterList, SemanticSyntaxInternal? returnSemantic, BlockSyntaxInternal body,
                                           DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 5;

        if (attributeLists != null)
        {
            AdjustWidth(attributeLists);
            _attributeLists = attributeLists;
        }

        AdjustWidth(returnType);
        ReturnType = returnType;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(parameterList);
        ParameterList = parameterList;

        if (returnSemantic != null)
        {
            AdjustWidth(returnSemantic);
            ReturnSemantic = returnSemantic;
        }

        AdjustWidth(body);
        Body = body;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new MethodDeclarationSyntaxInternal(Kind, _attributeLists, ReturnType, Identifier, ParameterList, ReturnSemantic, Body, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _attributeLists,
            1 => ReturnType,
            2 => Identifier,
            3 => ParameterList,
            4 => ReturnSemantic,
            5 => Body,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new MethodDeclarationSyntax(this, parent, position);
    }
}