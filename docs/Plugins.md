# Plugins

SharpX has plugin system for dynamically loading backend languages and transpilers.

## Language Plugin

A language plugin refers to an AST implementations of a back-end languages.  
They are loaded directly into the SharpX context and referenced by each transpile plugins.  
They are listed in the `languages` section of the config file and have a shared reference to `SharpX.Core.dll` and `SharpX.Composition.dll`.

## Transpile Plugin

A transpile plugin is a plugin for converting C# AST to AST of any language.  
They refer to the above language plugins and perform the conversion to the actual instances.  
Each Visitor methods in the transpile plugin are executed as follows:

```
A: Plugin.A.dll
B: Plugin.B.dll
```

1. Visit A's `VisitCompilationUnit(CSharpSyntaxNode)`
1. If A's one returns not-null:
   1. Visit A's `VisitCompilationUnit(CSharpSyntaxNode, TResult)`
   2. Visit B's `VisitCompilationUnit(CSharpSyntaxNode, TResult)`
1. If A's one returns null:
   1. Visit B's `VisitCompilationUnit(CSharpSyntaxNode)`
   2. Visit B's `VisitCompilationUnit(CSharpSyntaxNode, TResult)`

If the visitor method returns null, the process is padded on to the next lowest priority plugin.  
Otherwise; if a non-null value is returned, the process is passed to the method of the same name with two arguments.  
This means that the original node is passed as the first argument and the converted node as the second argument, and if further conversion is required, it is processed here.
