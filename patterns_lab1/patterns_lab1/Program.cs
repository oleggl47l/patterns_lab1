namespace patterns_lab1 {
    class Program {
        private static readonly PluginHandler PluginHandler = new PluginHandler();

        static void Main(string[] args) {
            //Некий пользовательский интерфйес
            while (true) {
                Console.WriteLine("Menu:");
                Console.WriteLine("1. Load plugins");
                Console.WriteLine("2. Unload plugin");
                Console.WriteLine("3. Execute plugin");
                Console.WriteLine("4. List plugins");
                Console.WriteLine("5. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice) {
                    case "1":
                        LoadPlugins();
                        break;
                    case "2":
                        UnloadPlugin();
                        break;
                    case "3":
                        ExecutePlugin();
                        break;
                    case "4":
                        ListPlugins();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Invalid choice, try again.");
                        break;
                }
            }
        }

        //Метод загрузки плагинов
        private static void LoadPlugins() {
            Console.Write("Enter the path to the plugin directory: ");
            string? path = Console.ReadLine();
            try {
                PluginHandler.LoadPlugins(path);
                Console.WriteLine("Plugins loaded successfully.");
            }
            catch (Exception ex) {
                Console.WriteLine($"Error: {ex.Message}");
            }
            Console.WriteLine("\n");
        }
        
        //Метод выгрузки плагинов
        private static void UnloadPlugin() {
            Console.Write("Enter the name of the plugin to unload: ");
            string? name = Console.ReadLine();
            PluginHandler.UnloadPlugin(name);
            Console.WriteLine("\n");
        }

        //Метод выполнения плагина
        private static void ExecutePlugin() {
            Console.Write("Enter the name of the plugin to execute: ");
            string? name = Console.ReadLine();
            PluginHandler.ExecutePlugin(name);
            Console.WriteLine("\n");
        }

        //метод получения списка плагинов
        private static void ListPlugins() {
            PluginHandler.ListPlugins();
            Console.WriteLine("\n");
        }
    }
}