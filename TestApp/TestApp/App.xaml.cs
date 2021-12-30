using App.Business;
using App.Services;
using App.UI.Views;
using System;
using DLToolkit.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App.Business.LotteryTicket;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace App.UI
{
	public partial class App : Application
	{
        #region Fields
        private static Service.Web.LottoService _lottoService;
        private static User _user;
        #endregion

        public App()
		{
			InitializeComponent();
            FlowListView.Init();
            _user = new User();
            //http://api.hubobel.de/lotto/Mittwoch
            _lottoService = new Service.Web.LottoService("https://www.lottoland.com.au/api/drawings/german6aus49");
            _user = _user.FromJSON();
            SetMainPage();
		}

        public static void SetMainPage() => Current.MainPage = new TabbedPage
        {
            Children =
                {
                    new NavigationPage(new LottoHomePage(_lottoService, _user))
                    {
                        //Title = "Startseite",
                        IconImageSource ="home_50.png"
                    },
                    new NavigationPage(new LottoUserView(_lottoService, _user))
                    {
                        //Title = "Benutzer",
                        IconImageSource = "customer_50.png"
                    },
                }
        };

        public static Page GetMainPage() => new NavigationPage(new LottoHomePage());
    }
}
