using System.Reflection;

namespace patterns_lab1;

public class PluginHandler {
    private List<IPlugin> Plugins { get; set; } = new();

    //Метод загрузки плагинов из указанной директории
    public void LoadPlugins(string path) {
        if (!Directory.Exists(path)) {
            throw new DirectoryNotFoundException($"Directory at path {path} doesn't exist");
        }

        //Получение списка файлов с расширением .dll из указанной директории
        string[] files = Directory.GetFiles(path, "*.dll");
        foreach (var file in files) {
            try {
                //Используем reflection
                //Загрузка сборки из файла
                var assembly = Assembly.LoadFile(file);
                //Получение всех типов из сборки, реализующих интерфейс IPlugin
                var types = assembly.GetTypes().Where(t =>
                    typeof(IPlugin).IsAssignableFrom(t) && !t.IsAbstract && !t.IsInterface);
                foreach (var type in types) {
                    //Создание экземпляра плагина и добавление его в список загруженных плагинов
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

    //Метод выгрузки плагина по имени
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

    //Метод получения плагина по имени
    private IPlugin GetPlugin(string name) =>
        Plugins.FirstOrDefault(p => p.Name == name) ??
        throw new InvalidOperationException($"Plugin {name} not found.");


    //Метод для выполнения операции, реализованной в плагине
    public void ExecutePlugin(string name) {
        var plugin = GetPlugin(name);
        plugin.Execute();
    }

    //Метод вывода списка загруженных плагинов на консоль
    public void ListPlugins() {
        Console.WriteLine("Loaded plugins:");
        foreach (var plugin in Plugins) {
            Console.WriteLine($"- {plugin.Name}");
        }
    }
}