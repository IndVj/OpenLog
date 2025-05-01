using Core.Application.Interfaces.Repository;
using Core.Domain.Entities;

namespace Infrastructure
{
    public class TodoRepository : ITodoRepository
    {
        private readonly ToDoDbContext dbContext;

        public TodoRepository(ToDoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task InitializeTodoContext()
        {
            try
            {
                await dbContext.WaitForConnection();
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<ToDoItem>> GetAllToDoItemsAsync()
        {
            await InitializeTodoContext();
            return dbContext.ToDoItems.ToList();
        }

        public ToDoItem? GetItemByIdentifier(Guid identifier)
        {
            return dbContext.ToDoItems.SingleOrDefault(t => t.Id == identifier);
        }

        public async Task AddToDoItemAsync(ToDoItem item)
        {
            dbContext.ToDoItems.Add(item);
            await dbContext.SaveChanges();
        }

        public async Task RemoveToDoItemAsync(ToDoItem item)
        {

            dbContext.ToDoItems.Remove(item);
            await dbContext.SaveChanges();
        }

        public async Task UpdateToDoItemAsync(ToDoItem item)
        {
            dbContext.ToDoItems.Remove(item);
            dbContext.ToDoItems.Add(item);
            await dbContext.SaveChanges();
        }


    }
}
