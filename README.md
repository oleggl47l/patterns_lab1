Структура проекта

- **patterns_lab1**: Основное консольное приложение, которое загружает и управляет плагинами.
  - `IPlugin.cs`: Интерфейс, который должны реализовывать все плагины.
  - `PluginHandler.cs`: Класс для загрузки, управления и выполнения плагинов.
  - `Program.cs`: Основной файл приложения с консольным интерфейсом.
- **PluginA**: Пример библиотеки классов, реализующей плагин.
  - `PluginA.cs`: Реализация плагина, наследующего интерфейс `IPlugin`.
