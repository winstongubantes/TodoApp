using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Data;
using TodoApp.Models;

namespace TodoApp.Services
{
    public class TodoService : ITodoService
    {
        private DataStore<TodoItem> _todoItemDataStore;

        public TodoService()
        {
            _todoItemDataStore = new DataStore<TodoItem>();
        }

        public async Task<IList<TodoItem>> GetTodoItems()
        {
            var result = await _todoItemDataStore.Query.ToListAsync();
            return result;
        }

        public async Task AddTodoItem(TodoItem item)
        {
            await _todoItemDataStore.InsertAsync(item);
        }

        public async Task UpdateTodoItem(TodoItem item)
        {
            await _todoItemDataStore.UpdateAsync(item);
        }

        public async Task DeleteTodoItem(TodoItem item)
        {
            await _todoItemDataStore.DeleteAsync(item);
        }
    }
}
