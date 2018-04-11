using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

using XamarinTodo.Models;

namespace XamarinTodo.Services
{
    public class TodoDataStoreFactory
    {
        string dbPath = "TodoSQLite.db3";
          
        public IDataStore<TodoItemModel> Create()
        {
            return CreateDiskDataStore();
        }

        public IDataStore<TodoItemModel> CreateDiskDataStore()
        {
            return new TodoDiskDataStore(DependencyService.Get<IFileHelper>().GetLocalFilePath(dbPath));
        }

        public IDataStore<TodoItemModel> CreateCacheDataStore()
        {
            return new TodoCacheDataStore();
        }
    }
}
