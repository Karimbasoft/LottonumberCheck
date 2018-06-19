using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LottoHomePage : ContentPage
	{
        private LottoHomePageViewModel viewModel;


        public LottoHomePage()
		{
			InitializeComponent ();
            BindingContext = viewModel = new LottoHomePageViewModel(this.Navigation);
        }

        public LottoHomePage(TestApp.Services.WebsideDataConverter websideDataConverter, TestApp.Business.User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new LottoHomePageViewModel(this.Navigation, websideDataConverter, user);
        }

        //async void Lottozahlen_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new LottoZahlenView());
        //}
    }
}