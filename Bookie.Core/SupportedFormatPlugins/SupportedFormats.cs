using Bookie.Common.Interfaces;
using Bookie.Common.Plugin;
using Bookie.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;

namespace Bookie.Core.SupportedFormatPlugins
{
    public class SupportedFormats : ISupportedFormats
    {
        public ObservableCollection<SupportedFormatPlugin> LoadedPlugins { get; set; }

        private readonly ILog _log;

        public SupportedFormats(ILog log)
        {
            _log = log;
        }

        public void LoadFromPath(string path, bool includeSubdirectories = false)
        {
            if (!Directory.Exists(path)) return;
            var dllFileNames = Directory.GetFiles(path, "*.dll", includeSubdirectories ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            ICollection<Assembly> assemblies = new List<Assembly>(dllFileNames.Length);
            foreach (var dllFile in dllFileNames)
            {
                try
                {
                    var an = AssemblyName.GetAssemblyName(dllFile);
                    var assembly = Assembly.Load(an);
                    assemblies.Add(assembly);
                }
                catch (Exception)
                {
                }
            }

            var pluginType = typeof(ISupportedFormatPlugin);
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

            LoadedPlugins = new ObservableCollection<SupportedFormatPlugin>();

            foreach (var type in pluginTypes)
            {
                var plugin = Activator.CreateInstance(type);
                var name = type.GetCustomAttributes(typeof(DisplayNameAttribute), false)[0].ToString();
                var description = type.GetCustomAttributes(typeof(DescriptionAttribute), false)[0].ToString();

                var wrappedPlugin = new SupportedFormatPlugin(plugin as ISupportedFormatPlugin, name, description);
                LoadedPlugins.Add(wrappedPlugin);
                _log.Info($"Loaded Supported Format Plugin : {name} : {type.FullName}");
            }
        }
    }
}