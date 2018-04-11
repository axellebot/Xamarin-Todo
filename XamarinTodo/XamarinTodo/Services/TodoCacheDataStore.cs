using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using XamarinTodo.Models;

namespace XamarinTodo.Services
{
    public class TodoCacheDataStore : IDataStore<TodoItemModel>
    {
        List<TodoItemModel> items;

        public TodoCacheDataStore()
        {
            items = new List<TodoItemModel>();
            var mockItems = new List<TodoItemModel>
            {
                new TodoItemModel { Id = Guid.NewGuid().ToString(), Name = "First task",Done = true },
                new TodoItemModel { Id = Guid.NewGuid().ToString(), Name = "Second task", Done = false },
                new TodoItemModel { Id = Guid.NewGuid().ToString(), Name = "Third task", Done = false },
                new TodoItemModel { Id = Guid.NewGuid().ToString(), Name = "Fourth task",  Done = true },
                new TodoItemModel { Id = Guid.NewGuid().ToString(), Name = "Fifth task",  Done = true},
                new TodoItemModel { Id = Guid.NewGuid().ToString(), Name = "Sixth task", Done= true },
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<int> AddItemAsync(TodoItemModel item)
        {
            items.Add(item);

            return await Task.FromResult(1);
        }

        public async Task<int> UpdateItemAsync(TodoItemModel todoItem)
        {
            var _todoItem = items.Where((TodoItemModel arg) => arg.Id == todoItem.Id).FirstOrDefault();
            items.Remove(_todoItem);
            items.Add(todoItem);

            return await Task.FromResult(1);
        }

        public async Task<int> DeleteItemAsync(TodoItemModel todoItem)
        {
            var _todoItem = items.Where((TodoItemModel arg) => arg.Id == todoItem.Id).FirstOrDefault();
            items.Remove(_todoItem);

            return await Task.FromResult(1);
        }

        public async Task<TodoItemModel> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<List<TodoItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}