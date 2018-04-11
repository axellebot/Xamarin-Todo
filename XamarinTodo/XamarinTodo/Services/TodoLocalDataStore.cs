using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

using SQLite;

using XamarinTodo.Models;

namespace XamarinTodo.Services
{
    public class TodoDiskDataStore : IDataStore<TodoItemModel>
    {
        SQLiteAsyncConnection database;

        public TodoDiskDataStore(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<TodoItemModel>().Wait();
        }
        
        public Task<int> AddItemAsync(TodoItemModel item)
        {
            return database.InsertAsync(item);
        }

        public Task<int> DeleteItemAsync(TodoItemModel item)
        {
            return database.DeleteAsync(item);
        }

        public Task<TodoItemModel> GetItemAsync(string id)
        {
            return database.Table<TodoItemModel>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<TodoItemModel>> GetItemsAsync(bool forceRefresh = false)
        {
            return database.Table<TodoItemModel>().ToListAsync();
        }

        public Task<int> UpdateItemAsync(TodoItemModel item)
        {
            return database.UpdateAsync(item);
        }
    }
}
