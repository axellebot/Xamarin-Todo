﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinTodo.UWP
{
	public partial class MainPage 
	{
		public MainPage()
		{
			InitializeComponent();

            LoadApplication(new XamarinTodo.App());
        }
    }
}
