namespace SharpX.Hlsl.SourceGenerator.TypeScript
{
    internal class Token
    {
        public SyntaxKind Kind { get; }

        public string Value { get; }

        public Token(SyntaxKind kind, string value)
        {
            Kind = kind;
            Value = value;
        }
    }
}
