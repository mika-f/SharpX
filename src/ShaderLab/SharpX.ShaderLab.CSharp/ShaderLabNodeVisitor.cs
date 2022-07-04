// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Diagnostics;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Core.Extensions;
using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;
using SharpX.ShaderLab.Primitives.Attributes.Compiler;
using SharpX.ShaderLab.Primitives.Enum;
using SharpX.ShaderLab.Syntax;

using AttributeSyntax = SharpX.ShaderLab.Syntax.AttributeSyntax;
using CompilationUnitSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax;
using ExpressionSyntax = SharpX.ShaderLab.Syntax.ExpressionSyntax;
using FieldDeclarationSyntax = SharpX.Hlsl.Syntax.FieldDeclarationSyntax;
using PropertyDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax;

namespace SharpX.ShaderLab.CSharp;

public class ShaderLabNodeVisitor : CompositeCSharpSyntaxVisitor<ShaderLabSyntaxNode>
{
    private readonly IBackendVisitorArgs<ShaderLabSyntaxNode> _args;
    private readonly List<FieldDeclarationSyntax> _globalFields;
    private int _level;

    public ShaderLabNodeVisitor(IBackendVisitorArgs<ShaderLabSyntaxNode> args) : base(args)
    {
        _args = args;
        _globalFields = new List<FieldDeclarationSyntax>();
    }

    public override ShaderLabSyntaxNode? DefaultVisit(SyntaxNode node)
    {
        // fallback to HLSL
        var source = _args.Invoke("HLSL", node);
        return source != null ? SyntaxFactory.HlslSource(source.NormalizeWhitespace()) : null;
    }

    public override ShaderLabSyntaxNode? VisitCompilationUnit(CompilationUnitSyntax node)
    {
        var members = node.Members.Select(Visit)
                          .Where(w => w != null)
                          .ToArray();
        if (members.Length == 0)
            return null;

        var decl = members[0];
        if (decl is ShaderDeclarationSyntax shader)
            return SyntaxFactory.CompilationUnit(shader);

        return members[0]; // returns HLSL source code that wrapped by ShaderLabSyntaxNode
    }

    public override ShaderLabSyntaxNode? VisitNamespaceDeclaration(NamespaceDeclarationSyntax node)
    {
        var members = node.Members.Select(Visit)
                          .Where(w => w != null)
                          .ToArray();

        if (members.Length != 1)
            return null;

        return members[0];
    }

    public override ShaderLabSyntaxNode? VisitFileScopedNamespaceDeclaration(FileScopedNamespaceDeclarationSyntax node)
    {
        var members = node.Members.Select(Visit)
                          .Where(w => w != null)
                          .ToArray();

        if (members.Length != 1)
            return null;

        return members[0];
    }

    public override ShaderLabSyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        try
        {
            _level++;

            if (HasShaderNameAttribute(node) && _level == 1)
            {
                var name = GetAttributeData(node, typeof(ShaderNameAttribute))[0][0] as string;
                if (string.IsNullOrWhiteSpace(name))
                    return null;

                var subShaders = new List<SubShaderDeclarationSyntax>();
                var members = node.Members.Select(Visit).Where(w => w != null).ToArray();
                var shaders = members.OfType<SubShaderDeclarationSyntax>().ToArray();
                subShaders.AddRange(shaders);

                var properties = members.OfType<Syntax.PropertyDeclarationSyntax>().ToArray();
                var propertiesDecl = members.Length > 0 ? SyntaxFactory.PropertiesDeclaration(properties) : null;

                var sources = new List<FieldDeclarationSyntax>();

                CgIncludeDeclarationSyntax? cgInclude = null;
                if (sources.Count > 0 || _globalFields.Count > 0)
                {
                    sources.AddRange(_globalFields.Select(w => w.NormalizeWhitespace().WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("    "))));
                    _globalFields.Clear();

                    var compilation = Hlsl.SyntaxFactory.CompilationUnit();
                    foreach (var member in sources)
                        compilation = compilation.AddMembers(member);

                    var includeSource = SyntaxFactory.HlslSource(compilation);
                    cgInclude = SyntaxFactory.CgIncludeDeclaration(includeSource);
                }

                return SyntaxFactory.ShaderDeclaration(name, propertiesDecl, cgInclude, SyntaxFactory.List(subShaders), null, null);
            }

            // subshader and pass

            // tags
            var tags = new List<TagDeclarationSyntax>();

            for (var i = 0; i < GetAttributeLength(node, typeof(ShaderTagAttribute)); i++)
            {
                var attr = GetAttributeData(node, typeof(ShaderTagAttribute), i);
                var name = attr[0][0] is int t ? Enum.GetName(typeof(ShaderTags), t) : attr[0][0]!.ToString();
                var value = attr[1][0] switch
                {
                    int v => name switch
                    {
                        "RenderType" => Enum.GetName(typeof(RenderType), v),
                        "Queue" => Enum.GetName(typeof(RenderQueue), v),
                        "PreviewType" => Enum.GetName(typeof(PreviewType), v),
                        "LightMode" => Enum.GetName(typeof(LightMode), v),
                        "PassFlags" => Enum.GetName(typeof(PassFlags), v),
                        "RequireOptions" => Enum.GetName(typeof(RequireOptions), v),
                        _ => null
                    },
                    _ => attr[1][0]!.ToString()
                };

                if (string.IsNullOrWhiteSpace(name) || string.IsNullOrWhiteSpace(value))
                    continue;

                tags.Add(SyntaxFactory.TagDeclaration(name, value));
            }

            var symbol = GetCurrentSymbol(node) as INamedTypeSymbol;
            if (symbol == null)
                return null;

            // commands
            var commands = ExtractCommands(node);

            if (_level == 2)
            {
                var passes = new List<BasePassDeclarationSyntax>();
                var members = node.Members.Select(Visit)
                                  .Where(w => w != null)
                                  .ToList();

                if (members.OfType<BasePassDeclarationSyntax>().Any())
                    passes.AddRange(members.OfType<BasePassDeclarationSyntax>());

                CgIncludeDeclarationSyntax? cgInclude = null;


                return SyntaxFactory.SubShaderDeclaration(SyntaxFactory.TagsDeclaration(tags.ToArray()), SyntaxFactory.List(commands.OfType<CommandDeclarationSyntax>()), cgInclude, SyntaxFactory.List(passes));
            }

            if (_level == 3)
            {
                var stencil = ExtractStencil(node);
                if (stencil != null)
                    commands.Add(stencil);

                switch (true)
                {
                    case { } when HasGrabPassAttribute(node):
                    {
                        var identifier = GetAttributeData(node, typeof(GrabPassAttribute))[0];
                        var tagDecl = tags.Count > 0 ? SyntaxFactory.TagsDeclaration(tags.ToArray()) : null;
                        if (identifier.Length == 1)
                            return SyntaxFactory.GrabPassDeclaration(identifier[0] as string, tagDecl, commands.OfType<NameDeclarationSyntax>().FirstOrDefault());
                        return SyntaxFactory.GrabPassDeclaration(null, tagDecl, commands.OfType<NameDeclarationSyntax>().FirstOrDefault());
                    }

                    case { } when HasRenderPassAttribute(node):
                    {
                        if (node.Members.Count != 0)
                        {
                            var hlsl = _args.Invoke("HLSL", node);
                            if (hlsl == null)
                                return null;

                            var program = SyntaxFactory.CgProgramDeclaration(hlsl);
                            var tagDecl = tags.Count > 0 ? SyntaxFactory.TagsDeclaration(tags.ToArray()) : null;
                            return SyntaxFactory.PassDeclaration(tagDecl, SyntaxFactory.List(commands), program);
                        }
                        else
                        {
                            var program = Hlsl.SyntaxFactory.CompilationUnit();
                            var attributes = GetAttributeData(node, typeof(ShaderProgramAttribute));

                            if (HasAttribute(node, typeof(ShaderVertexAttribute)))
                            {
                                var val = GetAttributeData(node, typeof(ShaderVertexAttribute))[0][0];
                                if (val is string str)
                                {
                                    var trivia = Hlsl.SyntaxFactory.PragmaDirectiveTrivia(Hlsl.SyntaxFactory.Identifier("vertex"), Hlsl.SyntaxFactory.Identifier(str))
                                                     .NormalizeWhitespace()
                                                     .WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("            "))
                                                     .WithTrailingTrivia(Hlsl.SyntaxFactory.EndOfLine("\n"));
                                    program = program.AddLeadingTrivia(Hlsl.SyntaxFactory.Trivia(trivia));
                                }
                            }

                            if (HasAttribute(node, typeof(ShaderFragmentAttribute)))
                            {
                                var val = GetAttributeData(node, typeof(ShaderFragmentAttribute))[0][0];
                                if (val is string str)
                                {
                                    var trivia = Hlsl.SyntaxFactory.PragmaDirectiveTrivia(Hlsl.SyntaxFactory.Identifier("fragment"), Hlsl.SyntaxFactory.Identifier(str))
                                                     .NormalizeWhitespace()
                                                     .WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("            "))
                                                     .WithTrailingTrivia(Hlsl.SyntaxFactory.EndOfLine("\n"));
                                    program = program.AddLeadingTrivia(Hlsl.SyntaxFactory.Trivia(trivia));
                                }
                            }

                            if (HasAttribute(node, typeof(ShaderIncludeAttribute)))
                            {
                                var val = GetAttributeData(node, typeof(ShaderIncludeAttribute))[0].OfType<string>();
                                foreach (var i in val)
                                {
                                    var trivia = Hlsl.SyntaxFactory.IncludeDirectiveTrivia(i)
                                                     .NormalizeWhitespace()
                                                     .WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("            "))
                                                     .WithTrailingTrivia(Hlsl.SyntaxFactory.EndOfLine("\n"));
                                    program = program.AddLeadingTrivia(Hlsl.SyntaxFactory.Trivia(trivia));
                                }
                            }

                            if (HasAttribute(node, typeof(ShaderProgramAttribute)))
                            {
                                var val = GetAttributeData(node, typeof(ShaderProgramAttribute))[0].OfType<INamedTypeSymbol>();
                                foreach (var t in val)
                                {
                                    var trivia = Hlsl.SyntaxFactory.IncludeDirectiveTrivia(_args.GetOutputFilePath(t))
                                                     .NormalizeWhitespace()
                                                     .WithLeadingTrivia(Hlsl.SyntaxFactory.Whitespace("            "))
                                                     .WithTrailingTrivia(Hlsl.SyntaxFactory.EndOfLine("\n"));
                                    program = program.AddLeadingTrivia(Hlsl.SyntaxFactory.Trivia(trivia));
                                }
                            }

                            foreach (var attribute in attributes)
                                Debug.WriteLine("");

                            var cgProgram = SyntaxFactory.CgProgramDeclaration(SyntaxFactory.HlslSource(program));
                            var tagDecl = tags.Count > 0 ? SyntaxFactory.TagsDeclaration(tags.ToArray()) : null;
                            return SyntaxFactory.PassDeclaration(tagDecl, SyntaxFactory.List(commands), cgProgram);
                        }
                    }

                    default:
                        return null;
                }
            }

            var source = _args.Invoke("HLSL", node)?.NormalizeWhitespace();
            if (source == null || string.IsNullOrWhiteSpace(source.ToFullString()))
                return null;
            return SyntaxFactory.HlslSource(source);
        }
        finally
        {
            _level--;
        }
    }

    private List<BaseCommandDeclarationSyntax> ExtractCommands(ClassDeclarationSyntax node)
    {
        var commands = new List<BaseCommandDeclarationSyntax>();

        if (HasAttribute(node, typeof(BlendAttribute)))
        {
            var args = GetAttributeData(node, typeof(BlendAttribute)).Select(w => Enum.GetName(typeof(BlendFunc), (int)w[0]!)!).ToList();
            if (args.Count == 2)
                commands.Add(SyntaxFactory.CommandDeclaration("Blend", $"{args[0]} {args[1]}"));
            else
                commands.Add(SyntaxFactory.CommandDeclaration("Blend", $"{args[0]} {args[1]}", $"{args[2]} {args[3]}"));
        }

        if (HasAttribute(node, typeof(CullingAttribute)))
        {
            var args = GetAttributeData(node, typeof(CullingAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(Culling), i)! : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("Cull", parameter));
        }

        if (HasAttribute(node, typeof(ZTestAttribute)))
        {
            var args = GetAttributeData(node, typeof(ZTestAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(ZTestFunc), i)! : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("ZTest", parameter));
        }

        if (HasAttribute(node, typeof(ZWriteAttribute)))
        {
            var args = GetAttributeData(node, typeof(ZWriteAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(ZWrite), i)! : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("ZWrite", parameter));
        }

        return commands;
    }

    private StencilDeclarationSyntax? ExtractStencil(ClassDeclarationSyntax node)
    {
        var commands = new List<CommandDeclarationSyntax>();

        if (HasAttribute(node, typeof(StencilRefAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilRefAttribute));
            var parameter = args[0][0] is int i ? i.ToString() : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("Ref", parameter));
        }

        if (HasAttribute(node, typeof(StencilReadMaskAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilReadMaskAttribute));
            var parameter = args[0][0] is int i ? i.ToString() : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("ReadMask", parameter));
        }

        if (HasAttribute(node, typeof(StencilWriteMaskAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilWriteMaskAttribute));
            var parameter = args[0][0] is int i ? i.ToString() : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("WriteMask", parameter));
        }

        if (HasAttribute(node, typeof(StencilCompareAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilCompareAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(CompareFunction), i) : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("Comp", parameter!));
        }

        if (HasAttribute(node, typeof(StencilPassAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilPassAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(StencilOp), i) : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("Pass", parameter!));
        }

        if (HasAttribute(node, typeof(StencilFailAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilFailAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(StencilOp), i) : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("Fail", parameter!));
        }

        if (HasAttribute(node, typeof(StencilZFailAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilZFailAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(StencilOp), i) : $"[{args[0][0]!}]";

            commands.Add(SyntaxFactory.CommandDeclaration("ZFail", parameter!));
        }

        if (HasAttribute(node, typeof(StencilAttribute)))
        {
            var args = GetNamedAttributeData(node, typeof(StencilAttribute));
            foreach (var arg in args)
                if (arg.Key.EndsWith("S"))
                    commands.Add(SyntaxFactory.CommandDeclaration(arg.Key[..^1], $"[{arg.Value}]"));
                else
                    commands.Add(SyntaxFactory.CommandDeclaration(arg.Key, arg.Value.ToString()!));
        }

        return commands.Any() ? SyntaxFactory.StencilDeclaration(commands.ToArray()) : null;
    }

    public override ShaderLabSyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node)
    {
        if (!node.Modifiers.Any(Microsoft.CodeAnalysis.CSharp.SyntaxKind.StaticKeyword))
            return null;

        var displayName = node.Identifier.ToFullString().Trim();
        var identifier = HasAttribute(node, typeof(NameAttribute)) ? GetAttributeData(node, typeof(NameAttribute))[0][0]!.ToString()! : displayName;
        var ut = GetUnityDeclaredTypeName(node);

        if (ut == null)
            return null;

        var t = SyntaxFactory.IdentifierName(ut);
        var @default = SyntaxFactory.EqualsValueClause(GetUnityDeclaredDefaultValue(ut, node));

        var attributeList = new List<AttributeSyntax>();
        var attributes = GetAttributes(node);
        if (attributes.Any(w => w.AttributeClass!.BaseType?.Equals(GetSymbol(typeof(PropertyAttribute)), SymbolEqualityComparer.Default) == true))
            foreach (var data in attributes.Where(w => w.AttributeClass!.BaseType?.Equals(GetSymbol(typeof(PropertyAttribute)), SymbolEqualityComparer.Default) == true))
            {
                var name = SyntaxFactory.IdentifierName(data.AttributeClass!.Name[..data.AttributeClass!.Name.LastIndexOf("Attribute", StringComparison.Ordinal)]);
                var argumentList = SyntaxFactory.ArgumentList();
                var attr = SyntaxFactory.Attribute(name, argumentList.Arguments.Count > 0 ? argumentList : null);
                attributeList.Add(attr);
            }

        var decl = SyntaxFactory.PropertyDeclaration(attributeList.Count > 0 ? SyntaxFactory.AttributeList(attributeList.ToArray()) : null, identifier, displayName, t, null, @default);

        if (HasAttribute(node, typeof(OnlyPropertyAttribute)))
            return decl;

        var ht = GetHlslType(ut);
        if (ht.StartsWith("MACRO:"))
            _globalFields.Add(Hlsl.SyntaxFactory.FieldDeclaration(Hlsl.SyntaxFactory.IdentifierName(""), Hlsl.SyntaxFactory.Identifier(ht.Replace("MACRO:", "").Replace("&s", identifier)))); // workaround for use MACRO in field declaration
        else
            _globalFields.Add(Hlsl.SyntaxFactory.FieldDeclaration(Hlsl.SyntaxFactory.IdentifierName(ht), Hlsl.SyntaxFactory.Identifier(identifier)));

        if (HasAttribute(node, typeof(OnlyDeclarationAttribute)))
            return null;

        return decl;
    }

    private string? GetUnityDeclaredTypeName(PropertyDeclarationSyntax node)
    {
        var t = GetDeclarationSymbol(node.Type);
        if (t == null)
            return null;

        if (t.Equals(GetSymbol(typeof(Sampler2D)), SymbolEqualityComparer.Default))
            return "2D";
        if (t.Equals(GetSymbol(typeof(Sampler3D)), SymbolEqualityComparer.Default))
            return "3D";
        if (t.Equals(GetSymbol(typeof(SamplerCUBE)), SymbolEqualityComparer.Default))
            return "CUBE";

        if (t.Equals(GetSymbol(typeof(Vector4<>)), SymbolEqualityComparer.Default))
        {
            if (HasAttribute(node, typeof(ColorAttribute)))
                return "Color";
            return "Vector";
        }

        if (t.Equals(GetSymbol(typeof(int)), SymbolEqualityComparer.Default))
        {
            if (HasAttribute(node, typeof(RangeAttribute)))
            {
                var args = GetAttributeData(node, typeof(RangeAttribute));
                return $"Range({args[0][0]}, {args[1][0]})";
            }

            return "Int";
        }

        if (t.Equals(GetSymbol(typeof(float)), SymbolEqualityComparer.Default))
        {
            if (HasAttribute(node, typeof(RangeAttribute)))
            {
                var args = GetAttributeData(node, typeof(RangeAttribute));
                return $"Range({args[0][0]}, {args[1][0]})";
            }

            return "Float";
        }

        return null;
    }

    private string GetHlslType(string t)
    {
        return t switch
        {
            "2D" => "MACRO:UNITY_DECLARE_TEX2D(&s)",
            "3D" => "MACRO:UNITY_DECLARE_TEX3D(&s)",
            "CUBE" => "MACRO:UNITY_DECLARE_TEXCUBE(&s)",
            "Color" => "float4",
            "Vector" => "float4",
            "Int" => "int",
            "Float" => "float",
            _ when t.StartsWith("Range") => "float",
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    private ExpressionSyntax GetUnityDeclaredDefaultValue(string t, PropertyDeclarationSyntax node)
    {
        var @default = HasAttribute(node, typeof(DefaultValueAttribute)) ? GetAttributeData(node, typeof(DefaultValueAttribute))[0][0]!.ToString()! : "";
        if (string.IsNullOrWhiteSpace(@default))
            @default = t is "Int" or "Float" or "Range" ? "0" : "";

        return t switch
        {
            "2D" => SyntaxFactory.TextureLiteralExpression(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.StringLiteral(@default))),
            "3d" => SyntaxFactory.TextureLiteralExpression(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.StringLiteral(@default))),
            "CUBE" => SyntaxFactory.TextureLiteralExpression(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.StringLiteral(@default))),
            "Color" => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(@default)),
            "Vector" => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(@default)),
            "Int" => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(int.Parse(@default))),
            "Float" => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(float.Parse(@default))),
            _ when t.StartsWith("Range") => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(float.Parse(@default))),
            _ => throw new ArgumentOutOfRangeException()
        };
    }

    #region Helpers

    #region Attributes

    private bool HasShaderNameAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(ShaderNameAttribute));
    }

    private bool HasSubShaderAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(SubShaderAttribute));
    }

    private bool HasShaderPassAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(ShaderPassAttribute));
    }

    private bool HasGrabPassAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(GrabPassAttribute));
    }

    private bool HasRenderPassAttribute(SyntaxNode node)
    {
        return HasAttribute(node, typeof(RenderPassAttribute));
    }

    private int GetAttributeLength(SyntaxNode node, Type t)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return 0;
        return GetAttributeLength(decl, t);
    }

    private int GetAttributeLength(ISymbol decl, Type t)
    {
        var s = _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        return decl.GetAttributes().Count(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true);
    }

    private List<object?[]> GetAttributeData(SyntaxNode node, Type t, int at = 0)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return new List<object?[]>();
        return GetAttributeData(decl, t, at);
    }

    private List<object?[]> GetAttributeData(ISymbol decl, Type t, int at = 0)
    {
        var s = _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attr = decl.GetAttributes().Where(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true).ElementAtOrDefault(at);
        if (attr == null)
            return new List<object?[]>();

        return attr.ConstructorArguments.Select(w =>
        {
            if (w.Type!.TypeKind == TypeKind.Array)
                return w.Values.Select(v => v.Value).ToArray();
            return new[] { w.Value };
        }).ToList();
    }

    private List<KeyValuePair<string, object>> GetNamedAttributeData(SyntaxNode node, Type t)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return new List<KeyValuePair<string, object>>();
        return GetNamedAttributeData(decl, t);
    }

    private List<KeyValuePair<string, object>> GetNamedAttributeData(ISymbol decl, Type t)
    {
        var s = _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attr = decl.GetAttributes().FirstOrDefault(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true);
        if (attr == null)
            return new List<KeyValuePair<string, object>>();

        return attr.NamedArguments.Select(w =>
        {
            var key = w.Key;
            var value = w.Value.Kind == TypedConstantKind.Array ? w.Value.Values : w.Value.Value!;

            return new KeyValuePair<string, object>(key, value);
        }).ToList();
    }

    private bool HasAttribute(SyntaxNode node, Type t, bool isReturnAttr = false)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return false;

        return HasAttribute(decl, t, isReturnAttr);
    }

    private bool HasAttribute(ISymbol decl, Type t, bool isReturnAttr = false)
    {
        var s = _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
        var attrs = isReturnAttr && decl is IMethodSymbol m ? m.GetReturnTypeAttributes() : decl.GetAttributes();
        return attrs.Any(w => w.AttributeClass?.Equals(s, SymbolEqualityComparer.Default) == true);
    }

    private List<AttributeData> GetAttributes(SyntaxNode node)
    {
        var decl = GetDeclarationSymbol(node);
        if (decl == null)
            return new List<AttributeData>();
        return GetAttributes(decl);
    }

    private List<AttributeData> GetAttributes(ISymbol symbol)
    {
        return symbol.GetAttributes().ToList();
    }

    private ISymbol? GetSymbol(Type t)
    {
        return _args.SemanticModel.Compilation.GetTypeByMetadataName(t.FullName ?? throw new ArgumentNullException());
    }

    private ISymbol? GetCurrentSymbol(SyntaxNode node)
    {
        var decl = _args.SemanticModel.GetDeclaredSymbol(node);
        if (decl != null)
            return decl;

        var info = _args.SemanticModel.GetSymbolInfo(node);
        if (info.Symbol is not INamedTypeSymbol baseDecl)
            return null;

        return baseDecl;
    }

    private ISymbol? GetDeclarationSymbol(SyntaxNode node)
    {
        var decl = _args.SemanticModel.GetDeclaredSymbol(node);
        if (decl != null)
            return decl;

        var info = _args.SemanticModel.GetSymbolInfo(node);
        if (info.Symbol is INamedTypeSymbol baseDecl)
            return baseDecl.ConstructedFrom;
        if (info.Symbol is IMethodSymbol methodDecl)
            return methodDecl;
        if (info.Symbol is IPropertySymbol propertyDecl)
            return propertyDecl;

        return null;
    }

    #endregion

    #endregion
}