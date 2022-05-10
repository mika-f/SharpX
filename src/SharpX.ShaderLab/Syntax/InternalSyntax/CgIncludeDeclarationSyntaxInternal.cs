using Microsoft.CodeAnalysis;

using SharpX.Core;

using SyntaxNode = SharpX.Core.SyntaxNode;

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

        public CgIncludeDeclarationSyntaxInternal(SyntaxKind kind, SyntaxTokenInternal cgIncludeKeyword, GreenNode source, SyntaxTokenInternal endCgKeyword, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
        {
            SlotCount = 3;

            AdjustWidth(cgIncludeKeyword);
            CgIncludeKeyword = cgIncludeKeyword;

            AdjustWidth(source);
            HlslSourceCode = source;

            AdjustWidth(endCgKeyword);
            EndCgKeyword = endCgKeyword;
        }

        public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
        {
            return new CgIncludeDeclarationSyntaxInternal(Kind, CgIncludeKeyword, HlslSourceCode, EndCgKeyword, GetDiagnostics(), annotations);
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new CgIncludeDeclarationSyntaxInternal(Kind, CgIncludeKeyword, HlslSourceCode, EndCgKeyword, diagnostics, GetAnnotations());
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
