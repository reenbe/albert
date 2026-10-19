
public class TodoService
{
    private readonly List<TodoModel> _tasks;
    private readonly ITaskRepository _repository;

    public TodoService(ITaskRepository repository)
    {
        _repository = repository;
        _tasks = _repository.Load();  
    }

    public List<TodoModel> GetAll()
    {
        return new List<TodoModel>(_tasks);
    }

    public void Add(string title, string description, DateTime dueDate, TodoPriority priority)
    {
        var task = new TodoModel
        {
            Id = GetNextId(),
            Title = title,
            Description = description,
            DueDate = dueDate,
            Priority = priority
        };

        _tasks.Add(task);
        _repository.Save(_tasks);
    }

    public void Edit(int id, string title, string description, DateTime dueDate, TodoPriority priority)
    {
        TodoModel task = FindById(id);

        task.Title = title;
        task.Description = description;
        task.DueDate = dueDate;
        task.Priority = priority;

        _repository.Save(_tasks);
    }

    public void Delete(int id)
    {
        TodoModel task = FindById(id);
        _tasks.Remove(task);
        _repository.Save(_tasks);
    }

    public void ChangeStatus(int id, TodoStatus status)
    {
        TodoModel task = FindById(id);
        task.Status = status;
        _repository.Save(_tasks);
    }

    private TodoModel FindById(int id)
    {
        foreach (var task in _tasks)
        {
            if (task.Id == id)
                return task;
        }
        throw new InvalidOperationException("Задача с таким номером не найдена.");
    }

    private int GetNextId()
    {
        int maxId = 0;
        foreach (var task in _tasks)
        {
            if (task.Id > maxId)
                maxId = task.Id;
        }
        return maxId + 1;
    }
}