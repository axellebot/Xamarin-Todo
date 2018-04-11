using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinTodo.ViewModels;

namespace XamarinTodo.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class TodoItemCell : ViewCell
	{
        // Needed by CellRenderer
        public TodoItemCell()
        {
            InitializeComponent();
        }

        public TodoItemCell (TodoItemCellViewModel viewModel)
		{
            BindingContext = viewModel;
			InitializeComponent ();
		}
	}
}