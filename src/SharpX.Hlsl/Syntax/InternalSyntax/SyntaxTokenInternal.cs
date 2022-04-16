using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax
{
    internal class SyntaxTokenInternal : HlslSyntaxNodeInternal
    {
        public SyntaxTokenInternal(SyntaxKind kind) : base(kind) { }
        public SyntaxTokenInternal(SyntaxKind kind, int fullWidth) : base(kind, fullWidth) { }
        public SyntaxTokenInternal(SyntaxKind kind, int fullWidth, DiagnosticInfo[]? diagnostics) : base(kind, fullWidth, diagnostics) { }
        public SyntaxTokenInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            throw new NotImplementedException();
        }

        public override GreenNode? GetSlot(int index)
        {
            throw new NotImplementedException();
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            throw new NotImplementedException();
        }
    }
}
