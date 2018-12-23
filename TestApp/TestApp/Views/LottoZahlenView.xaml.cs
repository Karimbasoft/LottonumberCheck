using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Business;
using App.Service.Web;
using App.Services;
using App.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LottoZahlenView : ContentPage
	{
        LottoZahlenViewModel viewModel;

        public LottoZahlenView (LottoService lottoService, User user)
		{
			InitializeComponent();
            BindingContext = viewModel = new LottoZahlenViewModel(lottoService, user);
        }

        public LottoZahlenView()
        {
            InitializeComponent();
        }
    }
}