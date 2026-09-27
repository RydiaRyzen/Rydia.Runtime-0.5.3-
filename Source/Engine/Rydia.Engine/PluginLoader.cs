using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Rydia.Engine
{

    /// <summary>
    /// 指定された型のプラグインを管理する機能を提供するクラスです
    /// </summary>
    /// <typeparam name="TPlugin">The type of the plugin.</typeparam>
    public abstract class PluginLoader<TPlugin> where TPlugin : IPlugin
    {

        /// <summary>
        /// プラグインのリストを取得します
        /// </summary>
        public List<TPlugin> Plugins { get; } = new List<TPlugin>();

        public string PluginFolderPath { get; set; } = "Plugins";

        public string PluginExtension { get; set; } = "*.dll";

        public void LoadPlugins()
        {
            if (!Directory.Exists(PluginFolderPath))
                return;

            var dllFiles = Directory.GetFiles(PluginFolderPath, PluginExtension);

            foreach (var file in dllFiles)
            {
                try
                {
                    var assembly = Assembly.LoadFrom(file);

                    var pluginTypes = assembly.GetTypes()
                        .Where(t => typeof(TPlugin).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract && t.GetConstructor(Type.EmptyTypes) != null);

                    foreach (var type in pluginTypes)
                    {
                        var plugin = (TPlugin)Activator.CreateInstance(type);
                        Plugins.Add(plugin);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Failed to load: {file} : {ex.Message}");
                }
            }
        }

    }
}
