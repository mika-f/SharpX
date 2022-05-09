using SharpX.Core;
using SharpX.ShaderLab.Syntax.InternalSyntax;

namespace SharpX.ShaderLab.Syntax
{
    public class ArgumentSyntax : ShaderLabSyntaxNode
    {
        private ExpressionSyntax? _expression;

        public ExpressionSyntax Expression => GetRedAtZero(ref _expression)!;

        internal ArgumentSyntax(ShaderLabSyntaxNodeInternal node, SyntaxNode? parent, int position) : base(node, parent, position) { }

        public override SyntaxNode? GetNodeSlot(int index)
        {
            return index == 0 ? GetRedAtZero(ref _expression) : null;
        }

        public override SyntaxNode? GetCachedSlot(int index)
        {
            return index == 0 ? _expression : null;
        }

        public ArgumentSyntax Update(ExpressionSyntax expression)
        {
            if (expression != Expression)
                return SyntaxFactory.Argument(expression);
            return this;
        }

        public ArgumentSyntax WithExpression(ExpressionSyntax expression)
        {
            return Update(expression);
        }

        public override TResult? Accept<TResult>(ShaderLabSyntaxVisitor<TResult> visitor) where TResult : default
        {
            return visitor.VisitArgument(this);
        }
    }
}
