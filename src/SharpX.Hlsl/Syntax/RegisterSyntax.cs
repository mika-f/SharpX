using SharpX.Core;
using SharpX.Hlsl.Syntax.InternalSyntax;

namespace SharpX.Hlsl.Syntax
{
    public class RegisterSyntax : HlslSyntaxNode
    {
        private IdentifierNameSyntax? _identifier;

        public SyntaxToken ColonToken => new(this, ((RegisterSyntaxInternal)Green).ColonToken, Position, 0);

        public SyntaxToken RegisterKeyword => new(this, ((RegisterSyntaxInternal)Green).RegisterKeyword, GetChildPosition(1), GetChildIndex(1));

        public SyntaxToken OpenParenToken => new(this, ((RegisterSyntaxInternal)Green).OpenParenToken, GetChildPosition(2), GetChildIndex(2));

        public IdentifierNameSyntax Register => GetRed(ref _identifier, 3)!;

        public SyntaxToken CloseParenToken => new(this, ((RegisterSyntaxInternal)Green).CloseParenToken, GetChildPosition(4), GetChildIndex(4));

        public RegisterSyntax(GreenNode node, SyntaxNode? parent, int position) : base(node, parent, position) { }

        public override SyntaxNode? GetNodeSlot(int index)
        {
            return index == 1 ? GetRed(ref _identifier, 1) : null;
        }

        public override SyntaxNode? GetCachedSlot(int index)
        {
            return index == 1 ? _identifier : null;
        }

        public RegisterSyntax Update(SyntaxToken colonToken, SyntaxToken registerKeyword, SyntaxToken openParenToken, IdentifierNameSyntax identifier, SyntaxToken closeParenToken)
        {
            if (colonToken != ColonToken || registerKeyword != RegisterKeyword || openParenToken != OpenParenToken || identifier != Register || closeParenToken != CloseParenToken)
                return SyntaxFactory.Register(colonToken, registerKeyword, openParenToken, identifier, closeParenToken);
            return this;
        }

        public RegisterSyntax WithColonToken(SyntaxToken colonToken)
        {
            return Update(colonToken, RegisterKeyword, OpenParenToken, Register, CloseParenToken);
        }

        public RegisterSyntax WithRegisterKeyword(SyntaxToken registerKeyword)
        {
            return Update(ColonToken, registerKeyword, OpenParenToken, Register, CloseParenToken);
        }

        public RegisterSyntax WithOpenParenToken(SyntaxToken openParenToken)
        {
            return Update(ColonToken, RegisterKeyword, openParenToken, Register, CloseParenToken);
        }

        public RegisterSyntax WithRegister(IdentifierNameSyntax identifier)
        {
            return Update(ColonToken, RegisterKeyword, OpenParenToken, identifier, CloseParenToken);
        }

        public RegisterSyntax WithCloseParenToken(SyntaxToken closeParenToken)
        {
            return Update(ColonToken, RegisterKeyword, OpenParenToken, Register, closeParenToken);
        }

        public override TResult? Accept<TResult>(HlslSyntaxVisitor<TResult> visitor) where TResult : default
        {
            return visitor.VisitRegister(this);
        }
    }
}
