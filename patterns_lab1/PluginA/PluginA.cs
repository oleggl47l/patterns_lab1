using patterns_lab1;

namespace PluginA;

public class PluginA : IPlugin {
    public string Name { get; set; } = "PluginA";
    public void Initialize() {
        Console.WriteLine("Plugin initialized");
    }

    public void Execute() {
        Console.WriteLine("Plugin executing");
    }

    public void Terminate() {
        Console.WriteLine("Plugin terminated");
    }
}