// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax;

internal class CommandDeclarationSyntaxInternal : BaseCommandDeclarationSyntaxInternal
{
    private readonly GreenNode _arguments;

    public override SyntaxTokenInternal Keyword { get; }

    public SeparatedSyntaxListInternal<IdentifierNameSyntaxInternal> Arguments => new(_arguments);

    public CommandDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, GreenNode arguments) : base(kind)
    {
        SlotCount = 2;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(arguments);
        _arguments = arguments;
    }

    public CommandDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal keyword, GreenNode arguments, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        AdjustWidth(keyword);
        Keyword = keyword;

        AdjustWidth(arguments);
        _arguments = arguments;
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new CommandDeclarationSyntaxInternal(Kind, Keyword, _arguments, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CommandDeclarationSyntaxInternal(Kind, Keyword, _arguments, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => Keyword,
            1 => _arguments,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new CommandDeclarationSyntax(this, parent, position);
    }
}