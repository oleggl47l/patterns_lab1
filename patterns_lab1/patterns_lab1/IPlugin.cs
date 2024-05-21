namespace patterns_lab1;


//Интерфейс IPlugin
public interface IPlugin {
    string Name { get; set; }
    void Initialize();
    void Execute();
    void Terminate();
}