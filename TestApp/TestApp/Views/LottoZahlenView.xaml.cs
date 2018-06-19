using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services;
using App.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LottoZahlenView : ContentPage
	{
        LottoZahlenViewModel viewModel;

        public LottoZahlenView (WebsideDataConverter websideDataConverter, TestApp.Business.User user)
		{
			InitializeComponent();
            BindingContext = viewModel = new LottoZahlenViewModel(websideDataConverter, user);
        }

        public LottoZahlenView()
        {
            InitializeComponent();
        }
    }
}