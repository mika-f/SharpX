// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using SharpX.Core;

namespace SharpX.Hlsl;

public enum SyntaxKind
{
    None = 0,

    List = GreenNode.ListKind,

    #region punctuation

    /// <summary>Represents <c>~</c> token.</summary>
    TildeToken = 10000,

    /// <summary>Represents <c>!</c> token.</summary>
    ExclamationToken,

    /// <summary>Represents <c>%</c> token.</summary>
    PercentToken,

    /// <summary>Represents <c>^</c> token.</summary>
    CaretToken,

    /// <summary>Represents <c>&amp;</c> token.</summary>
    AmpersandToken,

    /// <summary>Represents <c>*</c> token.</summary>
    AsteriskToken,

    /// <summary>Represents <c>(</c> token.</summary>
    OpenParenToken,

    /// <summary>Represents <c>)</c> token.</summary>
    CloseParenToken,

    /// <summary>Represents <c>-</c> token.</summary>
    MinusToken,

    /// <summary>Represents <c>+</c> token.</summary>
    PlusToken,

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

    /// <summary>Represents <c>\</c> token.</summary>
    BackslashToken,

    /// <summary>Represents <c>:</c> token.</summary>
    ColonToken,

    /// <summary>Represents <c>;</c> token.</summary>
    SemicolonToken,

    /// <summary>Represents <c>"</c> token.</summary>
    DoubleQuoteToken,

    /// <summary>Represents <c>'</c> token.</summary>
    SingleQuoteToken,

    /// <summary>Represents <c>&lt;</c> token.</summary>
    LessThanToken,

    /// <summary>Represents <c>,</c> token.</summary>
    CommaToken,

    /// <summary>Represents <c>&gt;</c> token.</summary>
    GreaterThanToken,

    /// <summary>Represents <c>.</c> token.</summary>
    DotToken,

    /// <summary>Represents <c>?</c> token.</summary>
    QuestionToken,

    /// <summary>Represents <c>#</c> token.</summary>
    HashToken,

    /// <summary>Represents <c>/</c> token.</summary>
    SlashToken,

    #endregion

    #region compound punctuation

    /// <summary>Represents <c>||</c> token.</summary>
    BarBarToken,

    /// <summary>Represents <c>&amp;&amp;</c> token.</summary>
    AmpersandAmpersandToken,

    /// <summary>Represents <c>--</c> token.</summary>
    MinusMinusToken,

    /// <summary>Represents <c>++</c> token.</summary>
    PlusPlusToken,

    /// <summary>Represents <c>!=</c> token.</summary>
    ExclamationEqualsToken,

    /// <summary>Represents <c>==</c> token.</summary>
    EqualsEqualsToken,

    /// <summary>Represents <c>&lt;=</c> token.</summary>
    LessThanEqualsToken,

    /// <summary>Represents <c>&lt;&lt;</c> token.</summary>
    LessThanLessThanToken,

    /// <summary>Represents <c>&lt;&lt;=</c> token.</summary>
    LessThanLessThanEqualsToken,

    /// <summary>Represents <c>&gt;=</c> token.</summary>
    GreaterThanEqualsToken,

    /// <summary>Represents <c>&gt;&gt;</c> token.</summary>
    GreaterThanGreaterThanToken,

    /// <summary>Represents <c>&gt;&gt;=</c> token.</summary>
    GreaterThanGreaterThanEqualsToken,

    /// <summary>Represents <c>/=</c> token.</summary>
    SlashEqualsToken,

    /// <summary>Represents <c>*=</c> token.</summary>
    AsteriskEqualsToken,

    /// <summary>Represents <c>|=</c> token.</summary>
    BarEqualsToken,

    /// <summary>Represents <c>&amp;=</c> token.</summary>
    AmpersandEqualsToken,

    /// <summary>Represents <c>+=</c> token.</summary>
    PlusEqualsToken,

    /// <summary>Represents <c>-=</c> token.</summary>
    MinusEqualsToken,

    /// <summary>Represents <c>^=</c> token.</summary>
    CaretEqualsToken,

    /// <summary>Represents <c>%=</c> token.</summary>
    PercentEqualsToken,

    #endregion

    #region keywords - see https: //docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-appendix-keywords

    /// <summary>Represents <c>AppendStructuredBuffer</c> token.</summary>
    AppendStructuredBufferKeyword,

    AsmKeyword,

    AsmFragmentKeyword,

    BlendStateKeyword,

    BoolKeyword,

    BreakKeyword,

    BufferKeyword,

    ByteAddressBufferKeyword,

    CaseKeyword,

    CBufferKeyword,

    CentroidKeyword,

    ClassKeyword,

    ColumnMajorKeyword,

    CompileKeyword,

    CompileFragmentKeyword,

    CompileShaderKeyword,

    ConstKeyword,

    ContinueKeyword,

    ComputeShaderKeyword,

    ConsumeStructuredBufferKeyword,

    DefKeyword,

    DefaultKeyword,

    DepthStencilStateKeyword,

    DepthStencilViewKeyword,

    DiscardKeyword,

    DoKeyword,

    DoubleKeyword,

    DomainShaderKeyword,

    DwordKeyword,

    ElseKeyword,

    ExportKeyword,

    ExternKeyword,

    FalseKeyword,

    FloatKeyword,

    ForKeyword,

    FxGroupKeyword,

    GeometryShaderKeyword,

    GroupSharedKeyword,

    HalfKeyword,

    HullShaderKeyword,

    IfKeyword,

    IncludeKeyword,

    InKeyword,

    InlineKeyword,

    InOutKeyword,

    InputPatchKeyword,

    IntKeyword,

    InterfaceKeyword,

    LineKeyword,

    LineAdjKeyword,

    LinearKeyword,

    LineStreamKeyword,

    MatrixKeyword,

    MessageKeyword,

    Min16FloatKeyword,

    Min10FloatKeyword,

    Min16IntKeyword,

    Min12IntKeyword,

    Min16UintKeyword,

    NamespaceKeyword,

    NoInterpolationKeyword,

    NoPerspectiveKeyword,

    NullKeyword,

    OutKeyword,

    OutputPatchKeyword,

    PackOffsetKeyword,

    PackMatrixKeyword,

    PassKeyword,

    PixelFragmentKeyword,

    PixelShaderKeyword,

    PointKeyword,

    PointStreamKeyword,

    PreciseKeyword,

    RasterizerStateKeyword,

    RenderTargetViewKeyword,

    ReturnKeyword,

    RegisterKeyword,

    RowMajorKeyword,

    // ReSharper disable once InconsistentNaming
    RWBufferKeyword,

    // ReSharper disable once InconsistentNaming
    RWByteAddressBufferKeyword,

    // ReSharper disable once InconsistentNaming
    RWStructuredBufferKeyword,

    // ReSharper disable once InconsistentNaming
    RWTexture1DKeyword,

    // ReSharper disable once InconsistentNaming
    RWTexture1DArrayKeyword,

    // ReSharper disable once InconsistentNaming
    RWTexture2DKeyword,

    // ReSharper disable once InconsistentNaming
    RWTexture2DArrayKeyword,

    // ReSharper disable once InconsistentNaming
    RWTexture3DKeyword,

    SampleKeyword,

    SamplerKeyword,

    SamplerStateKeyword,

    SamplerComparisonStateKeyword,

    SharedKeyword,

    SnormKeyword,

    StateBlockKeyword,

    StateBlockStateKeyword,

    StaticKeyword,

    StructKeyword,

    SwitchKeyword,

    StructuredBufferKeyword,

    // ReSharper disable once InconsistentNaming
    TBufferKeyword,

    TechniqueKeyword,

    Technique10Keyword,

    Technique11Keyword,

    TextureKeyword,

    Texture1DKeyword,

    Texture1DArrayKeyword,

    Texture2DKeyword,

    Texture2DArrayKeyword,

    // ReSharper disable once InconsistentNaming
    Texture2DMSKeyword,

    // ReSharper disable once InconsistentNaming
    Texture2DMSArrayKeyword,

    Texture3DKeyword,

    TextureCubeKeyword,

    TextureCubeArrayKeyword,

    TrueKeyword,

    TypedefKeyword,

    TriangleKeyword,

    TriangleAdjKeyword,

    TriangleStreamKeyword,

    UintKeyword,

    UniformKeyword,

    UnormKeyword,

    UnsignedKeyword,

    VectorKeyword,

    VertexFragmentKeyword,

    VertexShaderKeyword,

    VoidKeyword,

    VolatileKeyword,

    WhileKeyword,

    // preprocessor keywords - https://docs.microsoft.com/en-us/windows/win32/direct3dhlsl/dx-graphics-hlsl-appendix-preprocessor

    DefineKeyword,

    ElifKeyword,

    EndIfKeyword,

    WarningKeyword,

    ErrorKeyword,

    IfdefKeyword,

    IfndefKeyword,

    PragmaKeyword,

    UndefKeyword,

    #endregion

    #region tokens

    EndOfDirectiveToken,

    EndOfFileToken,

    IdentifierToken,

    NumericLiteralToken,

    CharacterLiteralToken,

    StringLiteralToken,

    IncludeReferenceLiteralToken,

    #endregion

    #region trivia

    EndOfLineTrivia,

    WhitespaceTrivia,

    SingleLineCommentTrivia,

    MultilineCommentTrivia,

    IfDirectiveTrivia,

    IfDefDirectiveTrivia,

    IfnDefDirectiveTrivia,

    ElifDirectiveTrivia,

    ElseDirectiveTrivia,

    EndIfDirectiveTrivia,

    DefineDirectiveTrivia,

    UndefDirectiveTrivia,

    WarningDirectiveTrivia,

    ErrorDirectiveTrivia,

    LineDirectiveTrivia,

    IncludeDirectiveTrivia,

    PragmaDefDirectiveTrivia,

    PragmaMessageDirectiveTrivia,

    PragmaPackMatrixDirectiveTrivia,

    PragmaWarningDirectiveTrivia,

    #endregion

    #region name & type-names

    IdentifierName,

    GenericName,

    TypeArgumentList,

    PredefinedType,

    ArrayType,

    ArrayRankSpecifier,

    #endregion

    #region expressions

    ParenthesizedExpression,

    ConditionalExpression,

    InvocationExpression,

    ElementAccessExpression,

    ArgumentList,

    BracketedArgumentList,

    Argument,

    CastExpression,

    ArrayInitializerExpression,

    ArrayCreationExpression,

    #endregion

    #region binary expressions

    AddExpression,

    SubtractExpression,

    MultiplyExpression,

    DivideExpression,

    ModuloExpression,

    LogicalOrExpression,

    LogicalAndExpression,

    BitwiseOrExpression,

    BitwiseAndExpression,

    ExclusiveOrExpression,

    EqualsExpression,

    NotEqualsExpression,

    LessThanExpression,

    LessThanOrEqualExpression,

    GreaterThanExpression,

    GreaterThanOrEqualExpression,

    SimpleMemberAccessExpression,

    #endregion

    #region binary assignment expression

    SimpleAssignmentExpression,

    AddAssignmentExpression,

    SubtractAssignmentExpression,

    MultiplyAssignmentExpression,

    DivideAssignmentExpression,

    ModuloAssignmentExpression,

    AndAssignmentExpression,

    ExclusiveOrAssignmentExpression,

    OrAssignmentExpression,

    LeftShiftAssignmentExpression,

    RightShiftAssignmentExpression,

    #endregion

    #region unary expression

    UnaryPlusExpression,

    UnaryMinusExpression,

    BitwiseNotExpression,

    LogicalNotExpression,

    PreIncrementExpression,

    PreDecrementExpression,

    PostIncrementExpression,

    PostDecrementExpression,

    IndexExpression,

    #endregion

    #region literal expressions

    NumericLiteralExpression,

    StringLiteralExpression,

    CharacterLiteralExpression,

    TrueLiteralExpression,

    FalseLiteralExpression,

    NullLiteralExpression,

    #endregion

    #region statements

    Block,

    LocalDeclarationStatement,

    VariableDeclaration,

    VariableDeclarator,

    EqualsValueClause,

    ExpressionStatement,

    EmptyStatement,

    #endregion

    #region jump statements

    BreakStatement,

    ContinueStatement,

    ReturnStatement,

    WhileStatement,

    DoStatement,

    ForStatement,

    #endregion

    #region checked statements

    IfStatement,

    ElseClause,

    SwitchStatement,

    SwitchSection,

    CaseSwitchLabel,

    DefaultSwitchLabel,

    #endregion

    #region declarations

    CompilationUnit,

    #endregion

    #region attributes

    AttributeList,

    Attribute,

    AttributeArgumentList,

    AttributeArgument,

    // attribute
    NameEquals,

    #endregion

    #region type declarations

    StructDeclaration,

    TechniqueDeclaration,

    PassDeclaration,

    FieldDeclaration,

    MethodDeclaration,

    Semantics,

    Register,

    ParameterList,

    Parameter,

    #endregion

    SingleVariableDesignation,

    DeclarationExpression
}