using System.Text;

Console.OutputEncoding = Encoding.UTF8;
Console.InputEncoding = Encoding.UTF8;


ITaskRepository repository = new JsonTaskRepository("tasks.json");
var service = new TodoService(repository);

Console.WriteLine("Загружено задач: " + service.GetAll().Count);

string PriorityText(TodoPriority p)
{
    if (p == TodoPriority.Low) return "Низкий";
    if (p == TodoPriority.Medium) return "Средний";
    return "Высокий";
}

string StatusText(TodoStatus s)
{
    if (s == TodoStatus.NotStarted) return "Не начата";
    if (s == TodoStatus.InProgress) return "В процессе";
    return "Выполнена";
}

int ReadInt(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (int.TryParse(Console.ReadLine(), out int number))
            return number;
        Console.WriteLine("Нужно ввести число.");
    }
}

DateTime ReadDate(string prompt)
{
    while (true)
    {
        Console.Write(prompt);
        if (DateTime.TryParse(Console.ReadLine(), out DateTime date))
            return date;
        Console.WriteLine("Неверная дата. Пример: 25.10.2026");
    }
}

TodoPriority ReadPriority()
{
    while (true)
    {
        int n = ReadInt("Приоритет (1 - Низкий, 2 - Средний, 3 - Высокий): ");
        if (n >= 1 && n <= 3)
            return (TodoPriority)(n - 1);
        Console.WriteLine("Введите 1, 2 или 3.");
    }
}

TodoStatus ReadStatus()
{
    while (true)
    {
        int n = ReadInt("Статус (1 - Не начата, 2 - В процессе, 3 - Выполнена): ");
        if (n >= 1 && n <= 3)
            return (TodoStatus)(n - 1);
        Console.WriteLine("Введите 1, 2 или 3.");
    }
}

void ShowTasks()
{
    var tasks = service.GetAll();
    if (tasks.Count == 0)
    {
        Console.WriteLine("Список задач пуст.");
        return;
    }

    foreach (var t in tasks)
    {
        Console.WriteLine($"[{t.Id}] {t.Title} | {StatusText(t.Status)} | приоритет: {PriorityText(t.Priority)}");
        Console.WriteLine($"     создана: {t.CreatedAt:dd.MM.yyyy}, срок: {t.DueDate:dd.MM.yyyy}");
        Console.WriteLine($"     описание: {t.Description}");
    }
}

// ---------- Главное меню ----------

while (true)
{
    Console.WriteLine();
    Console.WriteLine("=== Todo List ===");
    Console.WriteLine("1 - Показать задачи");
    Console.WriteLine("2 - Добавить задачу");
    Console.WriteLine("3 - Редактировать задачу");
    Console.WriteLine("4 - Удалить задачу");
    Console.WriteLine("5 - Изменить статус");
    Console.WriteLine("0 - Выход");
    Console.Write("Выбор: ");

    string choice = Console.ReadLine() ?? "";

    try
    {
        switch (choice)
        {
            case "1":
                ShowTasks();
                break;

            case "2":
                Console.Write("Название: ");
                string title = Console.ReadLine() ?? "";
                Console.Write("Описание: ");
                string description = Console.ReadLine() ?? "";
                DateTime dueDate = ReadDate("Срок выполнения (дд.мм.гггг): ");
                TodoPriority priority = ReadPriority();
                service.Add(title, description, dueDate, priority);
                Console.WriteLine("Задача добавлена.");
                break;

            case "3":
                int editId = ReadInt("Номер задачи: ");
                Console.Write("Новое название: ");
                string newTitle = Console.ReadLine() ?? "";
                Console.Write("Новое описание: ");
                string newDescription = Console.ReadLine() ?? "";
                DateTime newDue = ReadDate("Новый срок (дд.мм.гггг): ");
                TodoPriority newPriority = ReadPriority();
                service.Edit(editId, newTitle, newDescription, newDue, newPriority);
                Console.WriteLine("Задача изменена.");
                break;

            case "4":
                int deleteId = ReadInt("Номер задачи: ");
                service.Delete(deleteId);
                Console.WriteLine("Задача удалена.");
                break;

            case "5":
                int statusId = ReadInt("Номер задачи: ");
                TodoStatus status = ReadStatus();
                service.ChangeStatus(statusId, status);
                Console.WriteLine("Статус изменён.");
                break;

            case "0":
                return;

            default:
                Console.WriteLine("Нет такого пункта меню.");
                break;
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine("Ошибка: " + ex.Message);
    }
}