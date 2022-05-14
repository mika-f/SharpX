// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using SharpX.Composition.CSharp;
using SharpX.Composition.Interfaces;
using SharpX.ShaderLab.Primitives.Attributes;
using SharpX.ShaderLab.Primitives.Enum;
using SharpX.ShaderLab.Syntax;

using CompilationUnitSyntax = Microsoft.CodeAnalysis.CSharp.Syntax.CompilationUnitSyntax;

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

        return null; // returns HLSL source code that wrapped by ShaderLabSyntaxNode
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

                var members = node.Members.Select(Visit)
                                  .Where(w => w != null)
                                  .OfType<SubShaderDeclarationSyntax>()
                                  .ToArray();

                subShaders.AddRange(members);
            }

            return SyntaxFactory.ShaderDeclaration(name, null, null, SyntaxFactory.List(subShaders), null, null);
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


                    var cgProgram = SyntaxFactory.CgProgramDeclaration(SyntaxFactory.IdentifierName("a"));
                    var tagDecl = tags.Count > 0 ? SyntaxFactory.TagsDeclaration(tags.ToArray()) : null;
                    return SyntaxFactory.PassDeclaration(tagDecl, SyntaxFactory.List(commands), cgProgram);
                }

                default:
                    return null;
            }
        }

        return null;
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