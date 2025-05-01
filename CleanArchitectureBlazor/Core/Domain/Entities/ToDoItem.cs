namespace Core.Domain.Entities
{
    public class ToDoItem
    {
        public Guid Id { get; set; }  = Guid.NewGuid();
        public  string Title { get; set; }
        public bool IsCompleted { get; set; }
    }
}
