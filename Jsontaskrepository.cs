using System.Text.Json;

// Реализует интерфейс: хранит задачи в JSON-файле
public class JsonTaskRepository : ITaskRepository
{
    private readonly string _filePath;

    public JsonTaskRepository(string filePath)
    {
        _filePath = filePath;
    }

    public void Save(List<TodoModel> tasks)
    {
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(tasks, options);
        File.WriteAllText(_filePath, json);
    }

    public List<TodoModel> Load()
    {
        // Если файла ещё нет - просто пустой список
        if (!File.Exists(_filePath))
            return new List<TodoModel>();

        string json = File.ReadAllText(_filePath);
        return JsonSerializer.Deserialize<List<TodoModel>>(json) ?? new List<TodoModel>();
    }
}