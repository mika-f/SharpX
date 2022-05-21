using Microsoft.CodeAnalysis;

using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

using SyntaxNode = SharpX.Core.SyntaxNode;

namespace SharpX.Hlsl.Syntax.InternalSyntax
{
    internal class TopLevelModuleSyntaxInternal : MemberDeclarationSyntaxInternal
    {
        private readonly GreenNode? _members;

        public SyntaxListInternal<MemberDeclarationSyntaxInternal> Members => new(_members);

        public TopLevelModuleSyntaxInternal(SyntaxKind kind, GreenNode? members) : base(kind)
        {
            SlotCount = 1;

            if (members != null)
            {
                AdjustWidth(members);
                _members = members;
            }
        }

        public TopLevelModuleSyntaxInternal(SyntaxKind kind, GreenNode? members, DiagnosticInfo[]? diagnostics, SyntaxAnnotation[]? annotations) : base(kind, diagnostics, annotations)
        {
            SlotCount = 1;

            if (members != null)
            {
                AdjustWidth(members);
                _members = members;
            }
        }

        public override GreenNode SetAnnotations(SyntaxAnnotation[]? annotations)
        {
            return new TopLevelModuleSyntaxInternal(Kind, _members, GetDiagnostics(), annotations);
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new TopLevelModuleSyntaxInternal(Kind, _members, diagnostics, GetAnnotations());
        }

        public override GreenNode? GetSlot(int index)
        {
            return index == 0 ? _members : null;
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            return new TopLevelModuleSyntax(this, parent, position);
        }

        public override TResult? Accept<TResult>(HlslSyntaxVisitorInternal<TResult> visitor) where TResult : default
        {
            return visitor.VisitTopLevelModule(this);
        }
    }
}
