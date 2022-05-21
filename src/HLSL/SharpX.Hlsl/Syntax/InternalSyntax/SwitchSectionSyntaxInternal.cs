// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

internal class SwitchSectionSyntaxInternal : HlslSyntaxNodeInternal
{
    private readonly GreenNode? _labels;
    private readonly GreenNode? _statements;

    public SyntaxListInternal<SwitchLabelSyntaxInternal> Labels => new(_labels);

    public SyntaxListInternal<StatementSyntaxInternal> Statements => new(_statements);

    public SwitchSectionSyntaxInternal(SyntaxKind kind, GreenNode? labels, GreenNode? statements) : base(kind)
    {
        SlotCount = 2;

        if (labels != null)
        {
            AdjustWidth(labels);
            _labels = labels;
        }

        if (statements != null)
        {
            AdjustWidth(statements);
            _statements = statements;
        }
    }

    public SwitchSectionSyntaxInternal(SyntaxKind kind, GreenNode? labels, GreenNode? statements, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
    {
        SlotCount = 2;

        if (labels != null)
        {
            AdjustWidth(labels);
            _labels = labels;
        }

        if (statements != null)
        {
            AdjustWidth(statements);
            _statements = statements;
        }
    }

    public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
    {
        return new SwitchSectionSyntaxInternal(Kind, _labels, _statements, GetDiagnostics(), annotations);
    }

    public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
    {
        return new SwitchSectionSyntaxInternal(Kind, _labels, _statements, diagnostics, GetAnnotations());
    }

    public override GreenNode? GetSlot(int index)
    {
        return index switch
        {
            0 => _labels,
            1 => _statements,
            _ => null
        };
    }

    public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
    {
        return new SwitchSectionSyntax(this, parent, position);
    }

    public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
    {
        return visitor.VisitSwitchSection(this);
    }
}