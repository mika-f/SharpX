using System.Reflection;
using System.Runtime.Loader;

namespace SharpX.Compiler.Models
{
    internal class SharpXPluginHost : AssemblyLoadContext
    {
        private readonly AssemblyDependencyResolver _resolver;

        public SharpXPluginHost(string path, bool isCollectible = false) : base(isCollectible)
        {
            _resolver = new AssemblyDependencyResolver(path);
        }

        protected override Assembly? Load(AssemblyName assemblyName)
        {
            var path = _resolver.ResolveAssemblyToPath(assemblyName);
            if (string.IsNullOrWhiteSpace(path))
                return null;

            return LoadFromAssemblyPath(path);
        }

        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var path = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
            if (string.IsNullOrWhiteSpace(path))
                return IntPtr.Zero;

            return LoadUnmanagedDllFromPath(path);
        }
    }
}
