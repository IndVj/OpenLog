using Core.Domain.Entities;
using Core.Domain.ValueObjects;

namespace Core.Application.Interfaces.Services
{
    public interface IToDoService
    {
        Task<Result<bool>> AddItem(string title);
        
        Task<Result<List<ToDoItem>>> GetToDoItems();
        Task<Result<bool>> RemoveItem(ToDoItem item);
        Task<Result<bool>> UpdateItem(Guid identifier, string title, bool isCompleted);
    }
}