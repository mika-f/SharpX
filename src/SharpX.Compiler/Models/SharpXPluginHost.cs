// ------------------------------------------------------------------------------------------
//  Copyright (c) Natsuneko. All rights reserved.
//  Licensed under the MIT License. See LICENSE in the project root for license information.
// ------------------------------------------------------------------------------------------

using System.Reflection;
using System.Runtime.Loader;

namespace SharpX.Compiler.Models;

internal class SharpXPluginHost : AssemblyLoadContext
{
#if NET5_0_OR_GREATER
    private readonly AssemblyDependencyResolver _resolver;
#endif
    private readonly string _root;

#if NET5_0_OR_GREATER
    public SharpXPluginHost(string path, bool isCollectible = false) : base(Path.GetFileName(path), isCollectible)
#else
    public SharpXPluginHost(string path, bool isCollectible = false)
#endif
    {
        _root = Path.GetDirectoryName(path) ?? "";
#if NET5_0_OR_GREATER
        _resolver = new AssemblyDependencyResolver(path);
#endif
    }


    protected override Assembly? Load(AssemblyName assemblyName)
    {
#if NET5_0_OR_GREATER
        var path = _resolver.ResolveAssemblyToPath(assemblyName);
        if (string.IsNullOrWhiteSpace(path))
            return null;

        return LoadFromAssemblyPath(path);
#else
        return LoadFromAssemblyPath(Path.Combine(_root, $"{assemblyName.Name}.dll"));
#endif
    }

#if NET5_0_OR_GREATER
    protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
    {
        var path = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);
        if (string.IsNullOrWhiteSpace(path))
            return IntPtr.Zero;

        return LoadUnmanagedDllFromPath(path);
    }
#endif
}