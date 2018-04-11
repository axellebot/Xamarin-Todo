using System;

using Xamarin.Forms;

using XamarinTodo.Models;
using XamarinTodo.ViewModels;

namespace XamarinTodo.Views
{
	public partial class TodoListPage : ContentPage
    {
        TodoListViewModel viewModel;

        public TodoListPage()
		{
			InitializeComponent();
            viewModel = new TodoListViewModel();
            viewModel.navigation = Navigation;
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (viewModel.Items.Count == 0)
                viewModel.LoadItemsCommand.Execute(null);
        }
    }
}
