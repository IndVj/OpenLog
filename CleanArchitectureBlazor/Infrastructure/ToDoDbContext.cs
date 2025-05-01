using Core.Domain.Entities;
using IndexedDB.Blazor;
using Microsoft.JSInterop;

namespace Infrastructure
{
    public class ToDoDbContext : IndexedDb
    {
        public ToDoDbContext(IJSRuntime jSRuntime) : base(jSRuntime, "ToDoDatabase", 1) 
        {
            
        }

        public IndexedSet<ToDoItem> ToDoItems { get; set; }

    }
}
