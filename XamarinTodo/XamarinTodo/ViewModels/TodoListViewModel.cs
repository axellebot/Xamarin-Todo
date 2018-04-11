using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

using XamarinTodo.Models;
using XamarinTodo.Services;
using XamarinTodo.Views;

namespace XamarinTodo.ViewModels
{
    public class TodoListViewModel : BaseViewModel
    {
        private ObservableCollection<TodoItemCellViewModel> items;
        public ObservableCollection<TodoItemCellViewModel> Items {
            get { return items; }
            set { SetAndNotify(ref items, value); }
        }

        private TodoItemCellViewModel selectedItem;
        public TodoItemCellViewModel SelectedItem
        {
            get { return selectedItem; }
            set {
                SetAndNotify(ref selectedItem, value);
                if (selectedItem == null)
                    return;
                SelectItemCommand.Execute(selectedItem);
                SelectedItem = null;
            }
        }

        public ICommand LoadItemsCommand { get; private set; }
        public ICommand CreateItemCommand { get; private set; }
        public ICommand SelectItemCommand { get; private set; }

        public IDataStore<TodoItemModel> DataStore = (new TodoDataStoreFactory()).Create();

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetAndNotify(ref title, value); }
        }

        public INavigation navigation;

        public TodoListViewModel()
        {
            Title = "Todo List";
            Items = new ObservableCollection<TodoItemCellViewModel>();

            LoadItemsCommand = new Command(async () => await LoadItemsAsync());
            CreateItemCommand = new Command(async () => await CreateItemAsync());
            SelectItemCommand = new Command<TodoItemCellViewModel>(async (viewModel) => await SelecteItemAsync(viewModel));

            MessagingCenter.Subscribe<TodoItemCellViewModel, TodoItemModel>(this, "CheckTodoItem", async (obj, item) =>
            {
                var _item = item as TodoItemModel;
                await DataStore.UpdateItemAsync(_item);
            });

            MessagingCenter.Subscribe<TodoDetailViewModel, TodoItemModel>(this, "UpdateTodoItem", async (obj, item) =>
            {
                var _item = item as TodoItemModel;
                await DataStore.UpdateItemAsync(_item);
                await LoadItemsAsync();
            });

            MessagingCenter.Subscribe<TodoDetailViewModel, TodoItemModel>(this, "CreateTodoItem", async (obj, item) =>
            {
                var _item = item as TodoItemModel;
                await DataStore.AddItemAsync(_item);
                await LoadItemsAsync();
            });

            MessagingCenter.Subscribe<TodoDetailViewModel, TodoItemModel>(this, "DeleteTodoItem", async (obj, item) =>
            {
                var _item = item as TodoItemModel;
                await DataStore.DeleteItemAsync(_item);
                await LoadItemsAsync();
            });

            MessagingCenter.Subscribe<TodoItemCellViewModel, TodoItemModel>(this, "DeleteTodoItem", async (obj, item) =>
            {
                var _item = item as TodoItemModel;
                await DataStore.DeleteItemAsync(_item);
                await LoadItemsAsync();
            });
        }

        async Task MoreItemAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await SelectedItem?.MoreItemAsync();

            IsBusy = false;
        }
  
        private async Task LoadItemsAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            try
            {
                var items = await DataStore.GetItemsAsync(true);
                Items.Clear();
                foreach (var item in items)
                {
                    TodoItemCellViewModel viewModel = new TodoItemCellViewModel(item);
                    viewModel.navigation = navigation;
                    Items.Add(viewModel);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task CreateItemAsync()
        {
            await navigation.PushModalAsync(new NavigationPage(new TodoDetailPage(new TodoDetailViewModel())));
        }
        private async Task SelecteItemAsync(TodoItemCellViewModel todoItemCellViewModel)
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await todoItemCellViewModel.MoreItemAsync();

            IsBusy = false;
        }
    }
}
