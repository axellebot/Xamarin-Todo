using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using XamarinTodo.ViewModels;

namespace XamarinTodo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodoDetailPage : ContentPage
    {
        TodoDetailViewModel viewModel;

        public TodoDetailPage(TodoDetailViewModel viewModel)
        {
            InitializeComponent();

            viewModel.Title = "Edit Task";
            viewModel.navigation = this.Navigation;
            BindingContext = this.viewModel = viewModel;
        }

        public TodoDetailPage()
        {
            InitializeComponent();

            viewModel = new TodoDetailViewModel();

            viewModel.Title = "New Task";
            viewModel.navigation = this.Navigation;

            BindingContext = viewModel;
        }
    }
}