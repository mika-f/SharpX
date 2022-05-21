using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.ShaderLab.Syntax.InternalSyntax
{
    internal class HlslSourceSyntaxInternal : ShaderLabSyntaxNodeInternal
    {
        private readonly GreenNode? _sources;

        public SyntaxListInternal<GreenNode> Sources => new(_sources);

        public HlslSourceSyntaxInternal(SyntaxKind kind, GreenNode? sources) : base(kind)
        {
            SlotCount = 1;

            if (sources == null)
                return;

            AdjustWidth(sources);
            _sources = sources;
        }

        public HlslSourceSyntaxInternal(SyntaxKind kind, GreenNode? sources, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
        {
            SlotCount = 1;

            if (sources == null)
                return;

            AdjustWidth(sources);
            _sources = sources;
        }

        public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
        {
            return new HlslSourceSyntaxInternal(Kind, _sources, GetDiagnostics(), annotations);
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new HlslSourceSyntaxInternal(Kind, _sources, diagnostics, GetAnnotations());
        }

        public override GreenNode? GetSlot(int index)
        {
            return index == 0 ? _sources : null;
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            return new HlslSourceSyntax(this, parent, position);
        }
    }
}
