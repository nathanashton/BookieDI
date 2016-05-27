using Bookie.Common.Plugin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Bookie.Core
{
    public static class FormatPlugins<T>
    {
        public static ICollection<FormatPlugin> Load(string path)
        {
            if (!Directory.Exists(path)) return null;
            var dllFileNames = Directory.GetFiles(path, "*.dll");

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (var dllFile in dllFileNames)
            {
                var an = AssemblyName.GetAssemblyName(dllFile);
                var assembly = Assembly.Load(an);
                assemblies.Add(assembly);
            }

            var pluginType = typeof(T);
            ICollection<Type> pluginTypes = new List<Type>();
            foreach (var assembly in assemblies)
            {
                if (assembly == null) continue;
                var types = assembly.GetTypes();

                foreach (var t in types)
                {
                    if (t.IsInterface || t.IsAbstract)
                    {
                    }
                    else
                    {
                        var n = t.GetInterface(pluginType.FullName);
                        if (n != null)
                        {
                            pluginTypes.Add(t);
                        }
                    }
                }
            }

            ICollection<FormatPlugin> plugins = new List<FormatPlugin>(pluginTypes.Count);
            foreach (var type in pluginTypes)
            {
                var plugin = (T)Activator.CreateInstance(type);
                var name = type.GetCustomAttributes(typeof(DisplayNameAttribute), false)[0].ToString();
                var description = type.GetCustomAttributes(typeof(DescriptionAttribute), false)[0].ToString();

                var wrappedPlugin = new FormatPlugin(plugin as IFormatPlugin, name, description);

                plugins.Add(wrappedPlugin);
            }

            return plugins;
        }
    }
}