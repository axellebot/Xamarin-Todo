using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTodo.Models;
using XamarinTodo.Views;

namespace XamarinTodo.ViewModels
{
    public class TodoItemCellViewModel : BaseViewModel
    {
        public ICommand DeleteItemCommand { get; private set; }
        public ICommand CheckItemCommand { get; private set; }

        private TodoItemModel todoItem;
        public TodoItemModel TodoItem { get { return todoItem; } }

        public string Name { get { return todoItem.Name; } }
        public string Notes { get { return todoItem.Notes; } }

        public bool Done {
            get { return todoItem.Done; }
            set {
                todoItem.Done = value;
                CheckItemCommand.Execute(null);
                OnPropertyChanged("Done");
            }
        }

        public INavigation navigation;
        public TodoItemCellViewModel(TodoItemModel todoItem)
        {
            this.todoItem = todoItem;

            DeleteItemCommand = new Command(async () => await DeleteItemAsync());
            CheckItemCommand = new Command(async () => await ItemCheckedAsync());
        }

        public async Task DeleteItemAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            MessagingCenter.Send(this, "DeleteTodoItem", this.todoItem);

            IsBusy = false;
        }

       public async Task ItemCheckedAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            MessagingCenter.Send(this, "CheckTodoItem", this.todoItem);

            IsBusy = false;
        }
        public async Task MoreItemAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            TodoDetailViewModel viewModel = new TodoDetailViewModel(TodoItem);
            await navigation.PushAsync(new TodoDetailPage(viewModel));

            IsBusy = false;
        }
    }
}
