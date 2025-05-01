using Core.Domain.Entities;

namespace Core.Application.Interfaces.Repository
{
    public interface ITodoRepository
    {
        Task InitializeTodoContext();
        Task<List<ToDoItem>> GetAllToDoItemsAsync();
        ToDoItem? GetItemByIdentifier(Guid identifier);
        Task AddToDoItemAsync(ToDoItem item);
        Task RemoveToDoItemAsync(ToDoItem item);
        Task UpdateToDoItemAsync(ToDoItem item);

    }
}