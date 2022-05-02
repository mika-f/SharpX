using System.Collections.Generic;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax
{
    internal sealed class CompilationUnitSyntax
    {
        public List<MemberDeclarationSyntax> Members { get; }

        public CompilationUnitSyntax(List<MemberDeclarationSyntax> members)
        {
            Members = members;
        }
    }
}
