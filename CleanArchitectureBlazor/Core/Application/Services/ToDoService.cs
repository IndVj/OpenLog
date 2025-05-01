using Core.Application.Interfaces.Repository;
using Core.Application.Interfaces.Services;
using Core.Domain.Entities;
using Core.Domain.ValueObjects;

namespace Core.Application.Services
{
    public class ToDoService(ITodoRepository todoRepository) : IToDoService
    {
        private readonly ITodoRepository todoRepository = todoRepository;

        public async Task<Result<List<ToDoItem>>> GetToDoItems()
        {
            try
            {
                var allToDoItems = await this.todoRepository.GetAllToDoItemsAsync();

                return Result<List<ToDoItem>>.Success(allToDoItems);
            }
            catch (Exception ex)
            {
                return Result<List<ToDoItem>>.Failure(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Result<bool>> AddItem(string title)
        {
            try
            {
                var newItem = new ToDoItem { Title = title , IsCompleted = false};

                await this.todoRepository.AddToDoItemAsync(newItem);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Result<bool>> UpdateItem(Guid identifier, string title, bool isCompleted)
        {
            try
            {
                var existingItem = this.todoRepository.GetItemByIdentifier(identifier);

                if (existingItem != null)
                {
                    existingItem.Id = identifier;
                    existingItem.Title = title;
                    existingItem.IsCompleted = isCompleted;

                    await this.todoRepository.UpdateToDoItemAsync(existingItem);

                    return Result<bool>.Success(true);
                }
                else
                {
                    return Result<bool>.Failure($"Item:{title} deosnt exist in system");
                }
            }
            catch (Exception ex)
            {

                return Result<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
            }
        }

        public async Task<Result<bool>> RemoveItem(ToDoItem item)
        {
            try
            {
                await this.todoRepository.RemoveToDoItemAsync(item);

                return Result<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return Result<bool>.Failure(ex.InnerException?.Message ?? ex.Message);
            }
        }
    }
}
