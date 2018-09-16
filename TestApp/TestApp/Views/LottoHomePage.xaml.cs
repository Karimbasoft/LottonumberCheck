using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Business;
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

        public LottoHomePage(WebsideDataConverter websideDataConverter, User user)
        {
            InitializeComponent();
            CheckIfStartIsPossible(websideDataConverter, user);
        }

        private async void CheckIfStartIsPossible(WebsideDataConverter websideDataConverter , User user)
        {
            if (websideDataConverter.HtmlSourceCode == null || websideDataConverter.HtmlSourceCode.Equals(""))
            {
                if (!websideDataConverter.WebsideContent.CheckInternetConnection())
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
                BindingContext = viewModel = new LottoHomePageViewModel(this.Navigation, websideDataConverter, user);
            }
        }
        //async void Lottozahlen_Clicked(object sender, EventArgs e)
        //{
        //    await Navigation.PushAsync(new LottoZahlenView());
        //}
    }
}