using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using XamarinTodo.Models;
using XamarinTodo.Views;

namespace XamarinTodo.ViewModels
{
    public class TodoDetailViewModel : BaseViewModel
    {
        public ICommand SaveItemCommand { get; private set; }
        public ICommand DeleteItemCommand { get; private set; }
        public ICommand CancelItemCommand{ get; private set; }

        private string title = string.Empty;
        public string Title
        {
            get { return title; }
            set { SetAndNotify(ref title, value); }
        }

        private TodoItemModel todoItemModel;

        public bool Done
        {
            get { return todoItemModel.Done; }
            set
            {
                if (value != todoItemModel.Done)
                {
                    todoItemModel.Done = value;
                    OnPropertyChanged("Done");
                }
            }
        }

        public string Name
        {
            get { return todoItemModel.Name; }
            set
            {
                if (value != todoItemModel.Name)
                {
                    todoItemModel.Name = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        public string Notes
        {
            get { return todoItemModel.Notes; }
            set {
                if (value != todoItemModel.Notes) {
                    todoItemModel.Notes = value;
                    OnPropertyChanged("Notes");
                }
            }
        }

        private bool isNew;
        public INavigation navigation;
        public TodoDetailViewModel(TodoItemModel todoItemModel = null)
        {
            if (todoItemModel == null) {
                this.todoItemModel = new TodoItemModel();
                isNew = true;
            }
            else
            {
                this.todoItemModel = todoItemModel;
                isNew = false;
            }

            Title = Name;

            SaveItemCommand = new Command(async () => await SaveItemCommandAsync());
            DeleteItemCommand = new Command(async () => await DeleteItemCommandAsync());
            CancelItemCommand = new Command(async () => await CancelCommandAsync());
        }

        private async Task SaveItemCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (isNew) {
                MessagingCenter.Send(this, "CreateTodoItem", todoItemModel);
            }
            else
            {
                MessagingCenter.Send(this, "UpdateTodoItem", todoItemModel);
            }

            await BackAsync();

            IsBusy = false;
        }
        private async Task DeleteItemCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            if (!isNew) {
                MessagingCenter.Send(this, "DeleteTodoItem", todoItemModel);
            };

            await BackAsync();

            IsBusy = false;
        }

        private async Task CancelCommandAsync()
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await BackAsync();

            IsBusy = false;
        }

        private async Task BackAsync()
        {
            if (isNew)
            {
                await navigation.PopModalAsync();
            }
            else
            {
                await navigation.PopAsync();
            }
        }
    }
}
