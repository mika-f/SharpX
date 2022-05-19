// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.Core.Extensions;
using SharpX.Hlsl.Primitives.Attributes.Compiler;
using SharpX.Hlsl.Primitives.Types;
using SharpX.ShaderLab.Primitives.Attributes;
using SharpX.ShaderLab.Primitives.Enum;
using SharpX.ShaderLab.Syntax;

using CompilationUnitSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax;
using ExpressionSyntax = SharpX.ShaderLab.Syntax.ExpressionSyntax;
using PropertyDeclarationSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.PropertyDeclarationSyntax;

namespace SharpX.ShaderLab.CSharp;

public class ShaderLabNodeVisitor : CompositeCSharpSyntaxVisitor<ShaderLabSyntaxNode>
{
    private readonly IBackendVisitorArgs<ShaderLabSyntaxNode> _args;
    private readonly HashSet<INamedTypeSymbol> _subShaders;
    private readonly HashSet<INamedTypeSymbol> _passes;

    public ShaderLabNodeVisitor(IBackendVisitorArgs<ShaderLabSyntaxNode> args) : base(args)
    {
        _args = args;
        _subShaders = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
        _passes = new HashSet<INamedTypeSymbol>(SymbolEqualityComparer.Default);
    }

    public override ShaderLabSyntaxNode? DefaultVisit(SyntaxNode node)
    {
        return null;
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
        if (HasShaderNameAttribute(node))
        {
            var name = GetAttributeData(node, typeof(ShaderNameAttribute))[0][0] as string;
            if (string.IsNullOrWhiteSpace(name))
                return null;

            var subShaders = new List<SubShaderDeclarationSyntax>();

            if (HasSubShaderAttribute(node))
            {
                foreach (var t in GetAttributeData(node, typeof(SubShaderAttribute))[0].Where(w => w != null).OfType<INamedTypeSymbol>())
                    _subShaders.Add(t);

                var shaders = node.Members.Select(Visit)
                                  .Where(w => w != null)
                                  .OfType<SubShaderDeclarationSyntax>()
                                  .ToArray();

                subShaders.AddRange(shaders);
            }

            var members = node.Members.Select(Visit)
                              .Where(w => w != null)
                              .OfType<Syntax.PropertyDeclarationSyntax>()
                              .ToArray();

            var properties = members.Length > 0 ? SyntaxFactory.PropertiesDeclaration(members) : null;


            return SyntaxFactory.ShaderDeclaration(name, properties, null, SyntaxFactory.List(subShaders), null, null);
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

        if (_subShaders.Contains(symbol))
        {
            _subShaders.Remove(symbol);

            var passes = new List<BasePassDeclarationSyntax>();

            if (HasShaderPassAttribute(node))
                foreach (var t in GetAttributeData(node, typeof(ShaderPassAttribute))[0].Where(w => w != null).OfType<INamedTypeSymbol>())
                    _passes.Add(t);

            var members = node.Members.Select(Visit)
                              .Where(w => w != null)
                              .ToList();

            if (members.OfType<BasePassDeclarationSyntax>().Any())
                passes.AddRange(members.OfType<BasePassDeclarationSyntax>());

            CgIncludeDeclarationSyntax? cgInclude = null;


            return SyntaxFactory.SubShaderDeclaration(SyntaxFactory.TagsDeclaration(tags.ToArray()), SyntaxFactory.List(commands.OfType<CommandDeclarationSyntax>()), cgInclude, SyntaxFactory.List(passes));
        }

        if (_passes.Contains(symbol))
        {
            _passes.Remove(symbol);

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
                    var members = node.Members.Select(Visit)
                                      .Where(w => w != null)
                                      .ToList();


                    var cgProgram = members.Count == 0 ? SyntaxFactory.CgProgramDeclaration(SyntaxFactory.IdentifierName("a")) : SyntaxFactory.CgProgramDeclaration(SyntaxFactory.IdentifierName("b"));
                    var tagDecl = tags.Count > 0 ? SyntaxFactory.TagsDeclaration(tags.ToArray()) : null;
                    return SyntaxFactory.PassDeclaration(tagDecl, SyntaxFactory.List(commands), cgProgram);
                }

                default:
                    return null;
            }
        }

        var source = _args.Invoke("HLSL", node)?.NormalizeWhitespace();
        if (source == null || string.IsNullOrWhiteSpace(source.ToFullString()))
            return null;
        return SyntaxFactory.HlslSource(SyntaxFactory.List(source));
    }

    private List<BaseCommandDeclarationSyntax> ExtractCommands(ClassDeclarationSyntax node)
    {
        var commands = new List<BaseCommandDeclarationSyntax>();

        if (HasAttribute(node, typeof(BlendAttribute)))
        {
            var args = GetAttributeData(node, typeof(BlendAttribute));
            var chunk = args.Select(w => Enum.GetName(typeof(BlendFunc), (int)w[0]!)!).Chunk(args.Count == 4 ? 2 : 1);

            commands.Add(SyntaxFactory.CommandDeclaration("Blend", chunk.Select(w => w.Length == 2 ? w[0] + w[1] : w[0]).ToArray()));
        }

        if (HasAttribute(node, typeof(CullingAttribute)))
        {
            var args = GetAttributeData(node, typeof(CullingAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(Culling), i)! : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("Culling", parameter!));
        }

        if (HasAttribute(node, typeof(ZTestAttribute)))
        {
            var args = GetAttributeData(node, typeof(ZTestAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(ZTestFunc), i)! : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("ZTest", parameter!));
        }

        if (HasAttribute(node, typeof(ZWriteAttribute)))
        {
            var args = GetAttributeData(node, typeof(ZWriteAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(ZWrite), i)! : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("ZWrite", parameter!));
        }

        return commands;
    }

    private StencilDeclarationSyntax? ExtractStencil(ClassDeclarationSyntax node)
    {
        var commands = new List<CommandDeclarationSyntax>();

        if (HasAttribute(node, typeof(StencilRefAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilRefAttribute));
            var parameter = args[0][0] is int i ? i.ToString() : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("Ref", parameter!));
        }

        if (HasAttribute(node, typeof(StencilReadMaskAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilReadMaskAttribute));
            var parameter = args[0][0] is int i ? i.ToString() : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("ReadMask", parameter!));
        }

        if (HasAttribute(node, typeof(StencilWriteMaskAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilWriteMaskAttribute));
            var parameter = args[0][0] is int i ? i.ToString() : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("WriteMask", parameter!));
        }

        if (HasAttribute(node, typeof(StencilCompareAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilCompareAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(CompareFunction), i) : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("Comp", parameter!));
        }

        if (HasAttribute(node, typeof(StencilPassAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilPassAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(StencilOp), i) : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("Pass", parameter!));
        }

        if (HasAttribute(node, typeof(StencilFailAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilFailAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(StencilOp), i) : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("Fail", parameter!));
        }

        if (HasAttribute(node, typeof(StencilZFailAttribute)))
        {
            var args = GetAttributeData(node, typeof(StencilZFailAttribute));
            var parameter = args[0][0] is int i ? Enum.GetName(typeof(StencilOp), i) : args[0][0]!.ToString();

            commands.Add(SyntaxFactory.CommandDeclaration("ZFail", parameter!));
        }

        if (HasAttribute(node, typeof(StencilAttribute)))
        {
            var args = GetNamedAttributeData(node, typeof(StencilAttribute));
            foreach (var arg in args)
                if (arg.Key.EndsWith("S"))
                    commands.Add(SyntaxFactory.CommandDeclaration(arg.Key.Substring(0, arg.Key.Length - 1), $"[{arg.Value}]"));
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
        var n = GetUnityDeclaredTypeName(node);

        if (n == null)
            return null;

        var t = SyntaxFactory.IdentifierName(n);
        var @default = SyntaxFactory.EqualsValueClause(GetUnityDeclaredDefaultValue(n, node));
        var attributeList = new List<AttributeSyntax>();
        var attributes = GetAttributes(node);
        if (attributes.Any(w => w.AttributeClass!.BaseType?.Equals(GetSymbol(typeof(PropertyAttribute)), SymbolEqualityComparer.Default) == true))
            foreach (var data in attributes.Where(w => w.AttributeClass!.BaseType?.Equals(GetSymbol(typeof(PropertyAttribute)), SymbolEqualityComparer.Default) == true))
            {
                var name = SyntaxFactory.IdentifierName(data.AttributeClass!.Name.Substring(0, data.AttributeClass!.Name.LastIndexOf("Attribute", StringComparison.Ordinal)));
                var argumentList = SyntaxFactory.ArgumentList();
                var attr = SyntaxFactory.Attribute(name, argumentList.Arguments.Count > 0 ? argumentList : null);
                attributeList.Add(attr);
            }

        var decl = SyntaxFactory.PropertyDeclaration(attributeList.Count > 0 ? SyntaxFactory.AttributeList(attributeList.ToArray()) : null, identifier, displayName, t, null, @default);

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

    private ExpressionSyntax GetUnityDeclaredDefaultValue(string t, PropertyDeclarationSyntax node)
    {
        var @default = HasAttribute(node, typeof(DefaultValueAttribute)) ? GetAttributeData(node, typeof(DefaultValueAttribute))[0][0]!.ToString()! : "";

        return t switch
        {
            "2D" => SyntaxFactory.TextureLiteralExpression(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.StringLiteral(@default))),
            "3d" => SyntaxFactory.TextureLiteralExpression(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.StringLiteral(@default))),
            "CUBE" => SyntaxFactory.TextureLiteralExpression(SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.StringLiteral(@default))),
            "Color" => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(@default)),
            "Vector" => SyntaxFactory.LiteralExpression(SyntaxKind.StringLiteralExpression, SyntaxFactory.Literal(@default)),
            "Int" => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(1)),
            "Float" => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(1)),
            _ when t.StartsWith("Range") => SyntaxFactory.LiteralExpression(SyntaxKind.NumericLiteralExpression, SyntaxFactory.Literal(1)),
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