using System;
using System.Reflection;
using System.Runtime.Loader;

namespace PlugInclude
{
    /// <summary>
    /// Plugin loading helper
    /// </summary>
    public class PluginLoadContext : AssemblyLoadContext
    {
        /// <summary>
        /// Contains a dependency resolver
        /// </summary>
        private readonly AssemblyDependencyResolver _resolver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="pluginPath">Path to the plugin dll</param>
        public PluginLoadContext(string pluginPath)
        {
            _resolver = new AssemblyDependencyResolver(pluginPath);
        }

        /// <summary>
        /// Loads the assembly
        /// </summary>
        /// <param name="assemblyName">Name of the assembly to load</param>
        /// <returns>The assembly</returns>
        protected override Assembly Load(AssemblyName assemblyName)
        {
            var assemblyPath = _resolver.ResolveAssemblyToPath(assemblyName);

            return !string.IsNullOrWhiteSpace(assemblyPath) ? LoadFromAssemblyPath(assemblyPath) : null;
        }

        /// <summary>
        /// Loads an unmanaged dll
        /// </summary>
        /// <param name="unmanagedDllName">Name of the dll</param>
        /// <returns>Pointer</returns>
        protected override IntPtr LoadUnmanagedDll(string unmanagedDllName)
        {
            var libraryPath = _resolver.ResolveUnmanagedDllToPath(unmanagedDllName);

            return !string.IsNullOrWhiteSpace(libraryPath) ? LoadUnmanagedDllFromPath(libraryPath) : IntPtr.Zero;
        }


    }
}
