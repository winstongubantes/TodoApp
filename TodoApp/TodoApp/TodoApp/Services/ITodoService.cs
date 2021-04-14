using System.Collections.Generic;
using System.Threading.Tasks;
using TodoApp.Models;

namespace TodoApp.Services
{
    public interface ITodoService
    {
        Task<IList<TodoItem>> GetTodoItems();
        Task AddTodoItem(TodoItem item);
        Task UpdateTodoItem(TodoItem item);
        Task DeleteTodoItem(TodoItem item);
    }
}