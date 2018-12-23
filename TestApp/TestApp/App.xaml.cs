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
            _lottoService = new Service.Web.LottoService("https://www.gewinnspiel-gewinner.de/lottozahlen/");
            _user.UserNumbers = new System.Collections.ObjectModel.ObservableCollection<SparkleBox>(_user.DeserializeSparkleBoxList());
            SetMainPage();
		}

        public static void SetMainPage() => Current.MainPage = new TabbedPage
        {
            Children =
                {
                    new NavigationPage(new LottoHomePage(_lottoService, _user))
                    {
                        //Title = "Startseite",
                        Icon = Device.OnPlatform("home_50.png","home_50.png","home_50.png")
                    },
                    new NavigationPage(new LottoUserView(_lottoService, _user))
                    {
                        //Title = "Benutzer",
                        Icon = Device.OnPlatform("customer_50.png","customer_50.png","customer_50.png")
                    },
                }
        };

        public static Page GetMainPage() => new NavigationPage(new LottoHomePage());
    }
}
