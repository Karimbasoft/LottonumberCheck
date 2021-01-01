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
	public partial class LottoHomePage : ContentPage
	{
        private LottoHomePageViewModel viewModel;


        public LottoHomePage()
		{
			InitializeComponent ();
            BindingContext = viewModel = new LottoHomePageViewModel(this.Navigation);
        }

        public LottoHomePage(LottoService lottoService, User user)
        {
            InitializeComponent();
            CheckIfStartIsPossible(lottoService, user);
        }

        private async void CheckIfStartIsPossible(LottoService lottoService , User user)
        {
            if (lottoService.LottoWebside.HTML == null || lottoService.LottoWebside.HTML.Equals(""))
            {
                if (lottoService.LottoWebside.LottoWebside.Online)
                {
                    await DisplayAlert("Warnung", "Es konnte keine Internetverbindung aufgebaut werden !", "OK");
                }
                else
                {
                    await DisplayAlert("Warnung", "Ein unerwarteter Fehler ist aufgetretn!", "OK");
                }
            }
            else
            {
                BindingContext = viewModel = new LottoHomePageViewModel(Navigation, lottoService, user);
            }
        }
        //async void Lottozahlen_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new LottoZahlenView());
        //}
    }
}