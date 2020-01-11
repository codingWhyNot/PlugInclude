using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace PlugInclude
{
    /// <summary>
    /// Plugin manager
    /// </summary>
    public static class PluginManager
    {
        /// <summary>
        /// Loads the plugin from path
        /// </summary>
        /// <param name="pluginPath">Path to the plugin dll</param>
        /// <param name="mainType">Type of the class that manages to manage all plugin adds</param>
        public static Assembly LoadPlugin<T>(string relativePath, T mainType)
        {
            var root = Path.GetFullPath(Path.Combine(
                Path.GetDirectoryName(
                    Path.GetDirectoryName(
                        Path.GetDirectoryName(
                            Path.GetDirectoryName(
                                Path.GetDirectoryName(typeof(T).Assembly.Location)))))));

            var pluginLocation = Path.GetFullPath(Path.Combine(root, relativePath.Replace('\\', Path.DirectorySeparatorChar)));

            var loadContext = new PluginLoadContext(pluginLocation);

            return loadContext.LoadFromAssemblyName(new AssemblyName(Path.GetFileNameWithoutExtension(pluginLocation)));
        }

        /// <summary>
        /// Creates the plugin out of the assembly
        /// </summary>
        /// <param name="assembly">The plugin assembly</param>
        /// <returns>List of the plugins</returns>
        public static IEnumerable<IPlugin> CreatePlugins(Assembly assembly)
        {
            var pluginsLoaded = 0;

            foreach (var type in assembly.GetTypes())
            {
                if (!typeof(ICommand).IsAssignableFrom(type))
                    continue;

                if (!(Activator.CreateInstance(type) is IPlugin result))
                    continue;

                pluginsLoaded++;
                yield return result;
            }

            Console.WriteLine(pluginsLoaded + " Plugins found and loaded!");
        }
    }
}
