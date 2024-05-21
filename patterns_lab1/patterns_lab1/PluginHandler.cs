using System.Reflection;

namespace patterns_lab1;

public class PluginHandler {
    private List<IPlugin> Plugins { get; set; } = new();

    public void LoadPlugins(string path) {
        if (!Directory.Exists(path)) {
            throw new DirectoryNotFoundException($"Directory at path {path} doesn't exist");
        }

        string[] files = Directory.GetFiles(path, "*.dll");
        foreach (var file in files) {
            try {
                Assembly assembly = Assembly.LoadFile(file);
                var types = assembly.GetTypes().Where(t =>
                    typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
                foreach (var type in types) {
                    if (Activator.CreateInstance(type) is IPlugin plugin) {
                        plugin.Name = Path.GetFileNameWithoutExtension(file);
                        plugin.Initialize();
                        Plugins.Add(plugin);
                    }
                }
            }
            catch (Exception e) {
                Console.WriteLine($"Error loading plugin from file {file}: {e.Message}");
            }
        }
    }

    public void UnloadPlugin(string name) {
        var plugin = Plugins.FirstOrDefault(p => p.Name == name);
        if (plugin != null) {
            plugin.Terminate();
            Plugins.Remove(plugin);
            Console.WriteLine($"Plugin {name} unloaded successfully.");
        }
        else {
            Console.WriteLine($"Plugin {name} not found.");
        }
    }

    private IPlugin GetPlugin(string name) {
        return Plugins.FirstOrDefault(p => p.Name == name) ?? throw new InvalidOperationException($"Plugin {name} not found.");
    }

    public void ExecutePlugin(string name) {
        var plugin = GetPlugin(name);
        plugin.Execute();
    }

    public void ListPlugins() {
        Console.WriteLine("Loaded plugins:");
        foreach (var plugin in Plugins) {
            Console.WriteLine($"- {plugin.Name}");
        }
    }
}