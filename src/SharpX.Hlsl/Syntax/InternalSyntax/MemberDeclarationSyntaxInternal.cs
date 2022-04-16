using SharpX.Core;

namespace SharpX.Hlsl.Syntax.InternalSyntax
{
    internal abstract class MemberDeclarationSyntaxInternal : HlslSyntaxNodeInternal
    {
        protected MemberDeclarationSyntaxInternal(SyntaxKind kind) : base(kind) { }

        protected MemberDeclarationSyntaxInternal(SyntaxKind kind, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics) { }
    }
}
