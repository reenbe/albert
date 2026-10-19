
public enum TodoStatus
{
    NotStarted,   
    InProgress,   
    Done          
}

public enum TodoPriority
{
    Low,     
    Medium,   
    High      
}
 
public class TodoModel
{
    private string _title = "";
    private DateTime _dueDate = DateTime.Now;
 
    public int Id { get; set; }
 
    public string Title
    {
        get { return _title; }
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Название не может быть пустым.");
            _title = value;
        }
    }
 
    public string Description { get; set; } = "";
 
    public DateTime CreatedAt { get; set; } = DateTime.Now;
 
    public DateTime DueDate
    {
        get { return _dueDate; }
        set
        {
            if (value.Date < CreatedAt.Date)
                throw new ArgumentException("Срок не может быть раньше даты создания.");
            _dueDate = value;
        }
    }
 
    public TodoPriority Priority { get; set; } = TodoPriority.Medium;
 
    public TodoStatus Status { get; set; } = TodoStatus.NotStarted;
}
 