// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class VariableDeclarationSyntax : HlslSyntaxNode
{
    private TypeSyntax? _type;
    private SyntaxNode? _variables;

    public TypeSyntax Type => GetRedAtZero(ref _type)!;

    public SeparatedSyntaxList<VariableDeclaratorSyntax> Variables
    {
        get
        {
            var red = GetRed(ref _variables, 1);
            return red != null ? new SeparatedSyntaxList<VariableDeclaratorSyntax>(red, GetChildIndex(1)) : default;
        }
    }

    internal VariableDeclarationSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _type)!,
            1 => GetRed(ref _variables, 1)!,
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _type,
            1 => _variables,
            _ => null
        };
    }

    public VariableDeclarationSyntax Update(TypeSyntax type, SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
    {
        if (type != Type || variables != Variables)
            return SyntaxFactory.VariableDeclaration(type, variables);
        return this;
    }

    public VariableDeclarationSyntax WithType(TypeSyntax type)
    {
        return Update(type, Variables);
    }

    public VariableDeclarationSyntax WithVariables(SeparatedSyntaxList<VariableDeclaratorSyntax> variables)
    {
        return Update(Type, variables);
    }

    public VariableDeclarationSyntax AddVariables(params VariableDeclaratorSyntax[] items)
    {
        return WithVariables(Variables.AddRange(items));
    }
}