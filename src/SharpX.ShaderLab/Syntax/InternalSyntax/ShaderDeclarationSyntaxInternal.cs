// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class ShaderDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
{
    private readonly GreenNode _subShaders;

    public SyntaxTokenInternal ShaderKeyword { get; }

    public SyntaxTokenInternal Identifier { get; }

    public SyntaxTokenInternal OpenBraceToken { get; }

    public PropertiesDeclarationSyntaxInternal? Properties { get; }

    public CgIncludeDeclarationSyntaxInternal? CgInclude { get; }

    public SyntaxListInternal<SubShaderDeclarationSyntaxInternal> SubShaders => new(_subShaders);

    public FallbackDeclarationSyntaxInternal? Fallback { get; }

    public CustomEditorDeclarationSyntaxInternal? CustomEditor { get; }

    public SyntaxTokenInternal CloseBraceToken { get; }

    public ShaderDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal shaderKeyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, PropertiesDeclarationSyntaxInternal? properties, CgIncludeDeclarationSyntaxInternal? cgInclude, GreenNode subShaders,
                                           FallbackDeclarationSyntaxInternal? fallback,
                                           CustomEditorDeclarationSyntaxInternal? customEditor, SyntaxTokenInternal closeBraceToken) : base(kind)
    {
        SlotCount = 9;

        AdjustWidth(shaderKeyword);
        ShaderKeyword = shaderKeyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (properties != null)
        {
            AdjustWidth(properties);
            Properties = properties;
        }

        if (cgInclude != null)
        {
            AdjustWidth(cgInclude);
            CgInclude = cgInclude;
        }

        AdjustWidth(subShaders);
        _subShaders = subShaders;

        if (fallback != null)
        {
            AdjustWidth(fallback);
            Fallback = fallback;
        }

        if (customEditor != null)
        {
            AdjustWidth(customEditor);
            CustomEditor = customEditor;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public ShaderDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal shaderKeyword, SyntaxTokenInternal identifier, SyntaxTokenInternal openBraceToken, PropertiesDeclarationSyntaxInternal? properties, CgIncludeDeclarationSyntaxInternal? cgInclude, GreenNode subShaders,
                                           FallbackDeclarationSyntaxInternal? fallback, CustomEditorDeclarationSyntaxInternal? customEditor, SyntaxTokenInternal closeBraceToken, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 9;

        AdjustWidth(shaderKeyword);
        ShaderKeyword = shaderKeyword;

        AdjustWidth(identifier);
        Identifier = identifier;

        AdjustWidth(openBraceToken);
        OpenBraceToken = openBraceToken;

        if (properties != null)
        {
            AdjustWidth(properties);
            Properties = properties;
        }

        if (cgInclude != null)
        {
            AdjustWidth(cgInclude);
            CgInclude = cgInclude;
        }

        AdjustWidth(subShaders);
        _subShaders = subShaders;

        if (fallback != null)
        {
            AdjustWidth(fallback);
            Fallback = fallback;
        }

        if (customEditor != null)
        {
            AdjustWidth(customEditor);
            CustomEditor = customEditor;
        }

        AdjustWidth(closeBraceToken);
        CloseBraceToken = closeBraceToken;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new ShaderDeclarationSyntaxInternal(Kind, ShaderKeyword, Identifier, OpenBraceToken, Properties, CgInclude, _subShaders, Fallback, CustomEditor, CloseBraceToken, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new ShaderDeclarationSyntaxInternal(Kind, ShaderKeyword, Identifier, OpenBraceToken, Properties, CgInclude, _subShaders, Fallback, CustomEditor, CloseBraceToken, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => ShaderKeyword,
            1 => Identifier,
            2 => OpenBraceToken,
            3 => Properties,
            4 => CgInclude,
            5 => _subShaders,
            6 => Fallback,
            7 => CustomEditor,
            8 => CloseBraceToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new ShaderDeclarationSyntax(this, parent, position);
    }
}