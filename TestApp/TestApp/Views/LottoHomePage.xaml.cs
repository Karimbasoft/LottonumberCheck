using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Android.Util;
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
        private LottoHomePageViewModel _viewModel;
        private LottoService _lottoService;
        private User _user;

        public LottoHomePage()
		{
            try
            {
                InitializeComponent();
                BindingContext = _viewModel = new LottoHomePageViewModel(this.Navigation);
            }
            catch (Exception ex)
            {
                Log.Error("LottoAuswerter", ex.ToString());
            }
			
        }

        public LottoHomePage(LottoService lottoService, User user)
        {
            try
            {
                InitializeComponent();
                _lottoService = lottoService;
                _user = user;
            }
            catch (Exception ex)
            {
                Log.Error("LottoAuswerter", ex.ToString());
            }
            
        }

        private async void ContentPage_Appearing(object sender, EventArgs e)
        {
            await _lottoService.Initilize();
            if (_lottoService.LottoWebside.HTML == null || _lottoService.LottoWebside.HTML.Equals(""))
            {
                if (await InternetConnection.CheckInternetConnectionAsync(_lottoService.LottoWebside.LottoWebside.URL))
                {
                    await DisplayAlert("Warnung", "Es konnte keine Internetverbindung aufgebaut werden !", "OK");
                }
                else
                {
                    await DisplayAlert("Warnung", "Ein Fehler beim Download der Lottozahlen ist aufgetreten! (Net: True, Value: False)", "OK");
                }
            }
            else
            {
                BindingContext = _viewModel = new LottoHomePageViewModel(Navigation, _lottoService, _user);
            }
        }
    }
}