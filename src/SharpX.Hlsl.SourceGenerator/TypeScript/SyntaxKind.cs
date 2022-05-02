namespace SharpX.Hlsl.SourceGenerator.TypeScript
{
    internal enum SyntaxKind
    {
        None = 0,

        /// <summary>Represents <c>&amp;</c> token.</summary>
        AmpersandToken,

        /// <summary>Represents <c>(</c> token.</summary>
        OpenParenToken,

        /// <summary>Represents <c>)</c> token.</summary>
        CloseParenToken,

        /// <summary>Represents <c>=</c> token.</summary>
        EqualsToken,

        /// <summary>Represents <c>{</c> token.</summary>
        OpenBraceToken,

        /// <summary>Represents <c>}</c> token.</summary>
        CloseBraceToken,

        /// <summary>Represents <c>[</c> token.</summary>
        OpenBracketToken,

        /// <summary>Represents <c>]</c> token.</summary>
        CloseBracketToken,

        /// <summary>Represents <c>|</c> token.</summary>
        BarToken,

        /// <summary>Represents <c>:</c> token.</summary>
        ColonToken,

        /// <summary>Represents <c>;</c> token.</summary>
        SemicolonToken,

        /// <summary>Represents <c>&lt;</c> token.</summary>
        LessThanToken,

        /// <summary>Represents <c>,</c> token.</summary>
        CommaToken,

        /// <summary>Represents <c>&gt;</c> token.</summary>
        GreaterThanToken,

        AnyKeyword,

        ExtendsKeyword,

        ExportKeyword,

        TypeKeyword,

        Numeric,

        Identifier
    }
}
