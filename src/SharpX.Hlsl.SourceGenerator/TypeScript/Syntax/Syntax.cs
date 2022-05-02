using System.IO;

namespace SharpX.Hlsl.SourceGenerator.TypeScript.Syntax
{
    internal abstract class Syntax
    {
        public virtual string ToFullString()
        {
            var sw = new StringWriter();
            Write(sw);

            return sw.ToString();
        }

        public abstract void Write(TextWriter writer);
    }
}
