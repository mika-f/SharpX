using SharpX.Core;

namespace SharpX.ShaderLab.Syntax.InternalSyntax
{
    internal class CgIncludeDeclarationSyntaxInternal : ShaderLabSyntaxNodeInternal
    {
        public SyntaxTokenInternal CgIncludeKeyword { get; }

        public GreenNode HlslSourceCode { get; }

        public SyntaxTokenInternal EndCgKeyword { get; }

        public CgIncludeDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal cgIncludeKeyword, GreenNode source, SyntaxTokenInternal endCgKeyword) : base(kind)
        {
            SlotCount = 3;

            AdjustWidth(cgIncludeKeyword);
            CgIncludeKeyword = cgIncludeKeyword;

            AdjustWidth(source);
            HlslSourceCode = source;

            AdjustWidth(endCgKeyword);
            EndCgKeyword = endCgKeyword;
        }

        public CgIncludeDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal cgIncludeKeyword, GreenNode source, SyntaxTokenInternal endCgKeyword, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
        {
            SlotCount = 3;

            AdjustWidth(cgIncludeKeyword);
            CgIncludeKeyword = cgIncludeKeyword;

            AdjustWidth(source);
            HlslSourceCode = source;

            AdjustWidth(endCgKeyword);
            EndCgKeyword = endCgKeyword;
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new CgIncludeDeclarationSyntaxInternal(Kind, CgIncludeKeyword, HlslSourceCode, EndCgKeyword, diagnostics);
        }

        public override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => CgIncludeKeyword,
                1 => HlslSourceCode,
                2 => EndCgKeyword,
                _ => null
            };
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            return new CgIncludeDeclarationSyntax(this, parent, position);
        }
    }
}
