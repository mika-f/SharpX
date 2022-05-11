// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax;

public class ShaderDeclarationSyntax : ShaderLabSyntaxNode
{
    private PropertiesDeclarationSyntax? _properties;
    private CgIncludeDeclarationSyntax? _cgInclude;
    private SyntaxNode? _subShaders;
    private FallbackDeclarationSyntax? _fallback;
    private CustomEditorDeclarationSyntax? _customEditor;

    public SyntaxToken ShaderKeyword => new(this, ((ShaderDeclarationSyntaxInternal)Green).ShaderKeyword, Position, 0);

    public SyntaxToken Identifier => new(this, ((ShaderDeclarationSyntaxInternal)Green).Identifier, GetChildPosition(1), GetChildIndex(1));

    public SyntaxToken OpenBraceToken => new(this, ((ShaderDeclarationSyntaxInternal)Green).OpenBraceToken, GetChildPosition(2), GetChildIndex(2));

    public PropertiesDeclarationSyntax? Properties => GetRed(ref _properties, 3);

    public CgIncludeDeclarationSyntax? CgInclude => GetRed(ref _cgInclude, 4);

    public SyntaxList<SubShaderDeclarationSyntax> SubShaders
    {
        get
        {
            var red = GetRed(ref _subShaders, 5);
            return red != null ? new SyntaxList<SubShaderDeclarationSyntax>(red) : default;
        }
    }

    public FallbackDeclarationSyntax? Fallback => GetRed(ref _fallback, 6);

    public CustomEditorDeclarationSyntax? CustomEditor => GetRed(ref _customEditor, 7);

    public SyntaxToken CloseBraceToken => new(this, ((ShaderDeclarationSyntaxInternal)Green).CloseBraceToken, GetChildPosition(7), GetChildIndex(7));

    internal ShaderDeclarationSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            3 => GetRed(ref _properties, 3),
            4 => GetRed(ref _cgInclude, 4),
            5 => GetRed(ref _subShaders, 5),
            6 => GetRed(ref _fallback, 6),
            7 => GetRed(ref _customEditor, 7),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            3 => _properties,
            4 => _cgInclude,
            5 => _subShaders,
            6 => _fallback,
            7 => _customEditor,
            _ => null
        };
    }

    public ShaderDeclarationSyntax Update(SyntaxToken shaderKeyword, SyntaxToken identifier, SyntaxToken openBraceToken, PropertiesDeclarationSyntax? properties, CgIncludeDeclarationSyntax? cgInclude, SyntaxList<SubShaderDeclarationSyntax> subShaders, FallbackDeclarationSyntax? fallback,
                                          CustomEditorDeclarationSyntax? customEditor, SyntaxToken closeBraceToken)
    {
        if (shaderKeyword != ShaderKeyword || identifier != Identifier || openBraceToken != OpenBraceToken || properties != Properties || cgInclude != CgInclude || subShaders != SubShaders || fallback != Fallback || customEditor != CustomEditor || closeBraceToken != CloseBraceToken)
            return SyntaxFactory.ShaderDeclaration(shaderKeyword, identifier, openBraceToken, properties, cgInclude, subShaders, fallback, customEditor, closeBraceToken);
        return this;
    }

    public ShaderDeclarationSyntax WithShaderKeyword(SyntaxToken shaderKeyword)
    {
        return Update(shaderKeyword, Identifier, OpenBraceToken, Properties, CgInclude, SubShaders, Fallback, CustomEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithIdentifier(SyntaxToken identifier)
    {
        return Update(ShaderKeyword, identifier, OpenBraceToken, Properties, CgInclude, SubShaders, Fallback, CustomEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithOpenBraceToken(SyntaxToken openBraceToken)
    {
        return Update(ShaderKeyword, Identifier, openBraceToken, Properties, CgInclude, SubShaders, Fallback, CustomEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithProperties(PropertiesDeclarationSyntax? properties)
    {
        return Update(ShaderKeyword, Identifier, OpenBraceToken, properties, CgInclude, SubShaders, Fallback, CustomEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithCgInclude(CgIncludeDeclarationSyntax? cgInclude)
    {
        return Update(ShaderKeyword, Identifier, OpenBraceToken, Properties, cgInclude, SubShaders, Fallback, CustomEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithSubShaders(SyntaxList<SubShaderDeclarationSyntax> subShaders)
    {
        return Update(ShaderKeyword, Identifier, OpenBraceToken, Properties, CgInclude, subShaders, Fallback, CustomEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithFallback(FallbackDeclarationSyntax? fallback)
    {
        return Update(ShaderKeyword, Identifier, OpenBraceToken, Properties, CgInclude, SubShaders, fallback, CustomEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithCustomEditor(CustomEditorDeclarationSyntax? customEditor)
    {
        return Update(ShaderKeyword, Identifier, OpenBraceToken, Properties, CgInclude, SubShaders, Fallback, customEditor, CloseBraceToken);
    }

    public ShaderDeclarationSyntax WithCloseBraceToken(SyntaxToken closeBraceToken)
    {
        return Update(ShaderKeyword, Identifier, OpenBraceToken, Properties, CgInclude, SubShaders, Fallback, CustomEditor, closeBraceToken);
    }

    public ShaderDeclarationSyntax AddSubShaders(params SubShaderDeclarationSyntax[] items)
    {
        return WithSubShaders(SubShaders.AddRange(items));
    }

    public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
    {
        return visitor.VisitShaderDeclaration(this);
    }
}