﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LottoZahlenView : ContentPage
	{
        LottoZahlenViewModel viewModel;

        public LottoZahlenView (WebsideDataConverter websideDataConverter, Business.User user)
		{
			InitializeComponent ();
            BindingContext = viewModel = new LottoZahlenViewModel(websideDataConverter, user);
        }

        public LottoZahlenView()
        {
            InitializeComponent();
        }
    }
}