// Интерфейс: описывает, ЧТО нужно уметь (сохранять и загружать),
// но не говорит, КАК именно (в JSON, в базу и т.д.)
public interface ITaskRepository
{
    void Save(List<TodoModel> tasks);
    List<TodoModel> Load();
}
 