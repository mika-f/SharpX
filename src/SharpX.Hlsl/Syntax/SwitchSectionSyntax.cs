// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax;

public class SwitchSectionSyntax : HlslSyntaxNode
{
    private SyntaxNode? _labels;
    private SyntaxNode? _statements;

    public SyntaxList<SwitchLabelSyntax> Labels => new(GetRedAtZero(ref _labels));

    public SyntaxList<StatementSyntax> Statements => new(GetRed(ref _statements, 1));

    internal SwitchSectionSyntax(HlslSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

    public override SyntaxNode? GetNodeSlot(int index)
    {
        return index switch
        {
            0 => GetRedAtZero(ref _labels),
            1 => GetRed(ref _statements, 1),
            _ => null
        };
    }

    public override SyntaxNode? GetCachedSlot(int index)
    {
        return index switch
        {
            0 => _labels,
            1 => _statements,
            _ => null
        };
    }

    public SwitchSectionSyntax Update(SyntaxList<SwitchLabelSyntax> labels, SyntaxList<StatementSyntax> statements)
    {
        if (labels != Labels || statements != Statements)
            return SyntaxFactory.SwitchSection(labels, statements);
        return this;
    }

    public SwitchSectionSyntax WithLabels(SyntaxList<SwitchLabelSyntax> labels)
    {
        return Update(labels, Statements);
    }

    public SwitchSectionSyntax WithStatements(SyntaxList<StatementSyntax> statements)
    {
        return Update(Labels, statements);
    }

    public SwitchSectionSyntax AddLabels(params SwitchLabelSyntax[] items)
    {
        return WithLabels(Labels.AddRange(items));
    }

    public SwitchSectionSyntax AddStatements(params StatementSyntax[] items)
    {
        return WithStatements(Statements.AddRange(items));
    }
}