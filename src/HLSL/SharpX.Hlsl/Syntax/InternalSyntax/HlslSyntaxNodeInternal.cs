// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax;

[DebuggerDisplay("{GetDebuggerDisplay(), nq}")]
internal abstract class HlslSyntaxNodeInternal : GreenNode
{
    public override string Language => "HLSL";

    public SyntaxKind Kind => (SyntaxKind)RawKind;

    public override string KindText => Kind.ToString();

    public override int RawContextualKind => RawKind;

    public override bool IsStructuredTrivia => this is StructuredTriviaSyntaxInternal;

    public override bool IsDirective => this is DirectiveTriviaSyntaxInternal;

    protected HlslSyntaxNodeInternal(SyntaxKind kind) : base((int)kind) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, int fullWidth) : base((int)kind, fullWidth) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics) : base((int)kind, fullWidth, diagnostics) { }

    protected HlslSyntaxNodeInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base((int)kind, diagnostics) { }

    public override SyntaxToken CreateSeparator<TNode>(SyntaxNode element)
    {
        return Hlsl.SyntaxFactory.Token(SyntaxKind.CommaToken);
    }

    public override bool IsTriviaWithEndOfLine()
    {
        return Kind is SyntaxKind.EndOfLineTrivia or SyntaxKind.SingleLineCommentTrivia;
    }

    private static readonly ConditionalWeakTable<SyntaxNode, Dictionary<SyntaxTrivia, WeakReference<SyntaxNode?>>> StructureTable = new();

    public override SyntaxNode? GetStructure(SyntaxTrivia parentTrivia)
    {
        if (parentTrivia.HasStructure)
        {
            var parent = parentTrivia.Token.Parent;
            if (parent != null)
            {
                SyntaxNode? structure;
                var structsInParent = StructureTable.GetOrCreateValue(parent);
                lock (structsInParent)
                {
                    if (!structsInParent.TryGetValue(parentTrivia, out var weakStructure))
                    {
                        structure = StructuredTriviaSyntax.Create(parentTrivia);
                        structsInParent.Add(parentTrivia, new WeakReference<SyntaxNode?>(structure));
                    }
                    else if (!weakStructure.TryGetTarget(out structure))
                    {
                        structure = StructuredTriviaSyntax.Create(parentTrivia);
                        weakStructure.SetTarget(structure);
                    }
                }

                return structure!;
            }

            return StructuredTriviaSyntax.Create(parentTrivia)!;
        }

        return null;
    }

    public abstract TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor);

    public override bool TryCreateRed(SyntaxNode? parent, int position, [NotNullWhen(true)] out SyntaxNode? node)
    {
        node = CreateRed(parent, position);
        return true;
    }
}