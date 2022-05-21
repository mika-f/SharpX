// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

namespace SharpX.Hlsl.Syntax;

internal static class SyntaxFacts
{
    public static bool IsKeywordKind(SyntaxKind kind)
    {
        return kind is >= SyntaxKind.AppendStructuredBufferKeyword and <= SyntaxKind.WhileKeyword;
    }

    public static bool IsPreprocessorKeyword(SyntaxKind kind)
    {
        return kind is >= SyntaxKind.DefineKeyword and <= SyntaxKind.UndefKeyword;
    }

    public static bool IsPunctuation(SyntaxKind kind)
    {
        return kind is >= SyntaxKind.TildeToken and <= SyntaxKind.PercentEqualsToken;
    }

    public static bool IsPreprocessorDirective(SyntaxKind kind)
    {
        switch (kind)
        {
            case SyntaxKind.IfDirectiveTrivia:
            case SyntaxKind.IfDefDirectiveTrivia:
            case SyntaxKind.IfnDefDirectiveTrivia:
            case SyntaxKind.ElifDirectiveTrivia:
            case SyntaxKind.ElseDirectiveTrivia:
            case SyntaxKind.EndIfDirectiveTrivia:
            case SyntaxKind.DefineDirectiveTrivia:
            case SyntaxKind.UndefDirectiveTrivia:
            case SyntaxKind.WarningDirectiveTrivia:
            case SyntaxKind.ErrorDirectiveTrivia:
            case SyntaxKind.LineDirectiveTrivia:
            case SyntaxKind.IncludeDirectiveTrivia:
            case SyntaxKind.PragmaDefDirectiveTrivia:
            case SyntaxKind.PragmaMessageDirectiveTrivia:
            case SyntaxKind.PragmaPackMatrixDirectiveTrivia:
            case SyntaxKind.PragmaWarningDirectiveTrivia:
                return true;

            default:
                return false;
        }
    }

    public static bool IsLiteral(SyntaxKind currentKind)
    {
        switch (currentKind)
        {
            case SyntaxKind.IdentifierToken:
            case SyntaxKind.StringLiteralToken:
            case SyntaxKind.CharacterLiteralToken:
            case SyntaxKind.NumericLiteralToken:
            case SyntaxKind.IncludeReferenceLiteralToken:
                return true;

            default:
                return false;
        }
    }

    public static string GetText(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.TildeToken => "~",
            SyntaxKind.ExclamationToken => "!",
            SyntaxKind.PercentToken => "%",
            SyntaxKind.CaretToken => "^",
            SyntaxKind.AmpersandToken => "&",
            SyntaxKind.AsteriskToken => "*",
            SyntaxKind.OpenParenToken => "(",
            SyntaxKind.CloseParenToken => ")",
            SyntaxKind.MinusToken => "-",
            SyntaxKind.PlusToken => "+",
            SyntaxKind.EqualsToken => "=",
            SyntaxKind.OpenBraceToken => "{",
            SyntaxKind.CloseBraceToken => "}",
            SyntaxKind.OpenBracketToken => "[",
            SyntaxKind.CloseBracketToken => "]",
            SyntaxKind.BarToken => "|",
            SyntaxKind.BackslashToken => "\\",
            SyntaxKind.ColonToken => ":",
            SyntaxKind.SemicolonToken => ";",
            SyntaxKind.DoubleQuoteToken => "\"",
            SyntaxKind.SingleQuoteToken => "'",
            SyntaxKind.LessThanToken => "<",
            SyntaxKind.CommaToken => ",",
            SyntaxKind.GreaterThanToken => ">",
            SyntaxKind.DotToken => ".",
            SyntaxKind.QuestionToken => "?",
            SyntaxKind.HashToken => "#",
            SyntaxKind.SlashToken => "/",
            SyntaxKind.BarBarToken => "||",
            SyntaxKind.AmpersandAmpersandToken => "&&",
            SyntaxKind.MinusMinusToken => "--",
            SyntaxKind.PlusPlusToken => "++",
            SyntaxKind.ExclamationEqualsToken => "!=",
            SyntaxKind.EqualsEqualsToken => "==",
            SyntaxKind.LessThanEqualsToken => "<=",
            SyntaxKind.LessThanLessThanToken => "<<",
            SyntaxKind.LessThanLessThanEqualsToken => "<<=",
            SyntaxKind.GreaterThanEqualsToken => ">=",
            SyntaxKind.GreaterThanGreaterThanToken => ">>",
            SyntaxKind.GreaterThanGreaterThanEqualsToken => ">>=",
            SyntaxKind.SlashEqualsToken => "/=",
            SyntaxKind.AsteriskEqualsToken => "*=",
            SyntaxKind.BarEqualsToken => "|=",
            SyntaxKind.AmpersandEqualsToken => "%=",
            SyntaxKind.PlusEqualsToken => "+=",
            SyntaxKind.MinusEqualsToken => "-=",
            SyntaxKind.CaretEqualsToken => "^=",
            SyntaxKind.PercentEqualsToken => "%=",
            SyntaxKind.AppendStructuredBufferKeyword => "AppendStructuredBuffer",
            SyntaxKind.AsmKeyword => "asm",
            SyntaxKind.AsmFragmentKeyword => "asm_fragment",
            SyntaxKind.BlendStateKeyword => "BlendState",
            SyntaxKind.BoolKeyword => "bool",
            SyntaxKind.BreakKeyword => "break",
            SyntaxKind.BufferKeyword => "Buffer",
            SyntaxKind.ByteAddressBufferKeyword => "ByteAddressBuffer",
            SyntaxKind.CaseKeyword => "case",
            SyntaxKind.CBufferKeyword => "cbuffer",
            SyntaxKind.CentroidKeyword => "centroid",
            SyntaxKind.ClassKeyword => "class",
            SyntaxKind.ColumnMajorKeyword => "column_major",
            SyntaxKind.CompileKeyword => "compile",
            SyntaxKind.CompileFragmentKeyword => "compile_fragment",
            SyntaxKind.CompileShaderKeyword => "CompileShader",
            SyntaxKind.ConstKeyword => "const",
            SyntaxKind.ContinueKeyword => "continue",
            SyntaxKind.ComputeShaderKeyword => "ComputeShader",
            SyntaxKind.ConsumeStructuredBufferKeyword => "ConsumeStructuredBuffer",
            SyntaxKind.DefKeyword => "def",
            SyntaxKind.DefaultKeyword => "default",
            SyntaxKind.DepthStencilStateKeyword => "DepthStencilState",
            SyntaxKind.DepthStencilViewKeyword => "DepthStencilView",
            SyntaxKind.DiscardKeyword => "discard",
            SyntaxKind.DoKeyword => "do",
            SyntaxKind.DoubleKeyword => "double",
            SyntaxKind.DomainShaderKeyword => "DomainShader",
            SyntaxKind.DwordKeyword => "dword",
            SyntaxKind.ElseKeyword => "else",
            SyntaxKind.ExportKeyword => "export",
            SyntaxKind.ExternKeyword => "extern",
            SyntaxKind.FalseKeyword => "false",
            SyntaxKind.FloatKeyword => "float",
            SyntaxKind.ForKeyword => "for",
            SyntaxKind.FxGroupKeyword => "fxgroup",
            SyntaxKind.GeometryShaderKeyword => "GeometryShader",
            SyntaxKind.GroupSharedKeyword => "groupshared",
            SyntaxKind.HalfKeyword => "half",
            SyntaxKind.HullShaderKeyword => "Hullshader", // HullShader?
            SyntaxKind.IfKeyword => "if",
            SyntaxKind.IncludeKeyword => "include",
            SyntaxKind.InKeyword => "in",
            SyntaxKind.InlineKeyword => "inline",
            SyntaxKind.InOutKeyword => "inout",
            SyntaxKind.InputPatchKeyword => "InputPatch",
            SyntaxKind.IntKeyword => "int",
            SyntaxKind.InterfaceKeyword => "interface",
            SyntaxKind.LineKeyword => "line",
            SyntaxKind.LineAdjKeyword => "lineadj",
            SyntaxKind.LinearKeyword => "linear",
            SyntaxKind.LineStreamKeyword => "LineStream",
            SyntaxKind.MatrixKeyword => "matrix",
            SyntaxKind.MessageKeyword => "message",
            SyntaxKind.Min16FloatKeyword => "min16float",
            SyntaxKind.Min10FloatKeyword => "min10float",
            SyntaxKind.Min16IntKeyword => "min16int",
            SyntaxKind.Min12IntKeyword => "min12int",
            SyntaxKind.Min16UintKeyword => "min16uint",
            SyntaxKind.NamespaceKeyword => "namespace",
            SyntaxKind.NoInterpolationKeyword => "nointerpolation",
            SyntaxKind.NoPerspectiveKeyword => "noperspective",
            SyntaxKind.NullKeyword => "null",
            SyntaxKind.OutKeyword => "out",
            SyntaxKind.OutputPatchKeyword => "OutputPatch",
            SyntaxKind.PackOffsetKeyword => "packoffset",
            SyntaxKind.PackMatrixKeyword => "pack_matrix",
            SyntaxKind.PassKeyword => "pass",
            SyntaxKind.PixelFragmentKeyword => "pixelfragment",
            SyntaxKind.PixelShaderKeyword => "PixelShader",
            SyntaxKind.PointKeyword => "point",
            SyntaxKind.PointStreamKeyword => "PointStream",
            SyntaxKind.PreciseKeyword => "precise",
            SyntaxKind.RasterizerStateKeyword => "RasterizerState",
            SyntaxKind.RenderTargetViewKeyword => "RenderTargetView",
            SyntaxKind.ReturnKeyword => "return",
            SyntaxKind.RegisterKeyword => "register",
            SyntaxKind.RowMajorKeyword => "row_major",
            SyntaxKind.RWBufferKeyword => "RWBuffer",
            SyntaxKind.RWByteAddressBufferKeyword => "RWByteAddressBuffer",
            SyntaxKind.RWStructuredBufferKeyword => "RWStructuredBuffer",
            SyntaxKind.RWTexture1DKeyword => "RWTexture1D",
            SyntaxKind.RWTexture1DArrayKeyword => "RWTexture1DArray",
            SyntaxKind.RWTexture2DKeyword => "RWTexture2D",
            SyntaxKind.RWTexture2DArrayKeyword => "RWTexture2DArray",
            SyntaxKind.RWTexture3DKeyword => "RWTexture3D",
            SyntaxKind.SampleKeyword => "sample",
            SyntaxKind.SamplerKeyword => "sampler",
            SyntaxKind.SamplerStateKeyword => "SamplerState",
            SyntaxKind.SamplerComparisonStateKeyword => "SamplerComparisonState",
            SyntaxKind.SharedKeyword => "shared",
            SyntaxKind.SnormKeyword => "snorm",
            SyntaxKind.StateBlockKeyword => "stateblock",
            SyntaxKind.StateBlockStateKeyword => "stateblock_state",
            SyntaxKind.StaticKeyword => "static",
            SyntaxKind.StructKeyword => "struct",
            SyntaxKind.SwitchKeyword => "switch",
            SyntaxKind.StructuredBufferKeyword => "StructuredBuffer",
            SyntaxKind.TBufferKeyword => "tbuffer",
            SyntaxKind.TechniqueKeyword => "technique",
            SyntaxKind.Technique10Keyword => "technique10",
            SyntaxKind.Technique11Keyword => "technique11",
            SyntaxKind.TextureKeyword => "texture",
            SyntaxKind.Texture1DKeyword => "Texture1D",
            SyntaxKind.Texture1DArrayKeyword => "Texture1DArray",
            SyntaxKind.Texture2DKeyword => "Texture2D",
            SyntaxKind.Texture2DArrayKeyword => "Texture2DArray",
            SyntaxKind.Texture2DMSKeyword => "Texture2DMS",
            SyntaxKind.Texture2DMSArrayKeyword => "Texture2DMSArray",
            SyntaxKind.Texture3DKeyword => "Texture3D",
            SyntaxKind.TextureCubeKeyword => "TextureCube",
            SyntaxKind.TextureCubeArrayKeyword => "TextureCubeArray",
            SyntaxKind.TrueKeyword => "true",
            SyntaxKind.TypedefKeyword => "typedef",
            SyntaxKind.TriangleKeyword => "triangle",
            SyntaxKind.TriangleAdjKeyword => "triangleadj",
            SyntaxKind.TriangleStreamKeyword => "TriangleStream",
            SyntaxKind.UintKeyword => "uint",
            SyntaxKind.UniformKeyword => "uniform",
            SyntaxKind.UnormKeyword => "unorm",
            SyntaxKind.UnsignedKeyword => "unsigned",
            SyntaxKind.VectorKeyword => "vector",
            SyntaxKind.VertexFragmentKeyword => "vertexfragment",
            SyntaxKind.VertexShaderKeyword => "VertexShader",
            SyntaxKind.VoidKeyword => "void",
            SyntaxKind.VolatileKeyword => "volatile",
            SyntaxKind.WhileKeyword => "while",
            SyntaxKind.DefineKeyword => "define",
            SyntaxKind.ElifKeyword => "elif",
            SyntaxKind.EndIfKeyword => "endif",
            SyntaxKind.WarningKeyword => "warning",
            SyntaxKind.ErrorKeyword => "error",
            SyntaxKind.IfdefKeyword => "ifdef",
            SyntaxKind.IfndefKeyword => "ifndef",
            SyntaxKind.PragmaKeyword => "pragma",
            SyntaxKind.UndefKeyword => "undef",
            _ => string.Empty
        };
    }

    public static SyntaxKind GetAssignmentExpression(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.BarEqualsToken => SyntaxKind.OrAssignmentExpression,
            SyntaxKind.AmpersandEqualsToken => SyntaxKind.AndAssignmentExpression,
            SyntaxKind.CaretEqualsToken => SyntaxKind.ExclusiveOrAssignmentExpression,
            SyntaxKind.LessThanLessThanEqualsToken => SyntaxKind.LeftShiftAssignmentExpression,
            SyntaxKind.GreaterThanGreaterThanEqualsToken => SyntaxKind.RightShiftAssignmentExpression,
            SyntaxKind.PlusEqualsToken => SyntaxKind.AddAssignmentExpression,
            SyntaxKind.MinusEqualsToken => SyntaxKind.SubtractAssignmentExpression,
            SyntaxKind.AsteriskEqualsToken => SyntaxKind.MultiplyAssignmentExpression,
            SyntaxKind.SlashEqualsToken => SyntaxKind.DivideAssignmentExpression,
            SyntaxKind.PercentEqualsToken => SyntaxKind.ModuloAssignmentExpression,
            SyntaxKind.EqualsToken => SyntaxKind.SimpleAssignmentExpression,
            _ => SyntaxKind.None
        };
    }

    public static SyntaxKind GetBinaryExpression(SyntaxKind kind)
    {
        return kind switch
        {
            SyntaxKind.BarToken => SyntaxKind.BitwiseOrExpression,
            SyntaxKind.CaretToken => SyntaxKind.ExclusiveOrExpression,
            SyntaxKind.AmpersandToken => SyntaxKind.BitwiseAndExpression,
            SyntaxKind.EqualsEqualsToken => SyntaxKind.EqualsExpression,
            SyntaxKind.ExclamationEqualsToken => SyntaxKind.NotEqualsExpression,
            SyntaxKind.LessThanExpression => SyntaxKind.LessThanExpression,
            SyntaxKind.LessThanEqualsToken => SyntaxKind.LessThanOrEqualExpression,
            SyntaxKind.GreaterThanToken => SyntaxKind.GreaterThanExpression,
            SyntaxKind.GreaterThanEqualsToken => SyntaxKind.GreaterThanOrEqualExpression,
            SyntaxKind.PlusToken => SyntaxKind.AddExpression,
            SyntaxKind.MinusToken => SyntaxKind.SubtractExpression,
            SyntaxKind.AsteriskToken => SyntaxKind.MultiplyExpression,
            SyntaxKind.SlashToken => SyntaxKind.DivideExpression,
            SyntaxKind.PercentToken => SyntaxKind.ModuloExpression,
            SyntaxKind.AmpersandAmpersandToken => SyntaxKind.LogicalAndExpression,
            SyntaxKind.BarBarToken => SyntaxKind.LogicalOrExpression,
            _ => SyntaxKind.None
        };
    }
}