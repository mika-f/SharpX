// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class CompilationUnitSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode? _members;

    public SyntaxListInternal<MemberDeclarationSyntaxInternal> Members => new(_members);

    public SyntaxTokenInternal EndOfFileToken { get; }

    public CompilationUnitSyntaxInternal(SyntaxKind kind, GreenNode? members, SyntaxTokenInternal endOfFileToken) : base(kind)
    {
        SlotCount = 2;

        if (members != null)
        {
            AdjustWidth(members);
            _members = members;
        }

        AdjustWidth(endOfFileToken);
        EndOfFileToken = endOfFileToken;
    }

    public CompilationUnitSyntaxInternal(SyntaxKind kind, GreenNode? members, SyntaxTokenInternal endOfFileToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
    {
        SlotCount = 2;

        if (members != null)
        {
            AdjustWidth(members);
            _members = members;
        }

        AdjustWidth(endOfFileToken);
        EndOfFileToken = endOfFileToken;
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new CompilationUnitSyntaxInternal(Kind, _members, EndOfFileToken, diagnostics);
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _members,
            1 => EndOfFileToken,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new CompilationUnitSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitCompilationUnit(this);
    }
}