using SharpX.Core;
using SharpX.Core.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax.InternalSyntax
{
    internal class ContinueStatementSyntaxInternal : StatementSyntaxInternal
    {
        private readonly GreenNode? _attributeLists;

        public override SyntaxListInternal<AttributeListSyntaxInternal> AttributeLists => new(_attributeLists);

        public SyntaxTokenInternal ContinueKeyword { get; }

        public SyntaxTokenInternal SemicolonToken { get; }


        public ContinueStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal continueKeyword, SyntaxTokenInternal semicolonToken) : base(kind)
        {
            SlotCount = 3;

            if (attributeLists != null)
            {
                AdjustWidth(attributeLists);
                _attributeLists = attributeLists;
            }

            AdjustWidth(continueKeyword);
            ContinueKeyword = continueKeyword;

            AdjustWidth(semicolonToken);
            SemicolonToken = semicolonToken;
        }

        public ContinueStatementSyntaxInternal(SyntaxKind kind, GreenNode? attributeLists, SyntaxTokenInternal continueKeyword, SyntaxTokenInternal semicolonToken, DiagnosticInfo[]? diagnostics) : base(kind, diagnostics)
        {
            SlotCount = 3;

            if (attributeLists != null)
            {
                AdjustWidth(attributeLists);
                _attributeLists = attributeLists;
            }

            AdjustWidth(continueKeyword);
            ContinueKeyword = continueKeyword;

            AdjustWidth(semicolonToken);
            SemicolonToken = semicolonToken;
        }

        public override GreenNode SetDiagnostics(DiagnosticInfo[]? diagnostics)
        {
            return new ContinueStatementSyntaxInternal(Kind, _attributeLists, ContinueKeyword, SemicolonToken, diagnostics);
        }

        public override GreenNode? GetSlot(int index)
        {
            return index switch
            {
                0 => _attributeLists,
                1 => ContinueKeyword,
                2 => SemicolonToken,
                _ => null
            };
        }

        public override SyntaxNode CreateRed(SyntaxNode? parent, int position)
        {
            return new ContinueStatementSyntax(this, parent, position);
        }
    }
}
