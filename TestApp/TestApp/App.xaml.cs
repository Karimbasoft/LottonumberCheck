using DLToolkit.Forms.Controls;
using TestApp.Views;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TestApp
{
	public partial class App : Application
	{
        public App()
		{
			InitializeComponent();
            FlowListView.Init();
            dataConverter = new Services.WebsideDataConverter();
            user = new Business.User();
            user.UserNumbers = new System.Collections.ObjectModel.ObservableCollection<Business.SparkleBox>(user.DeserializeSparkleBoxList());
            SetMainPage();
		}

        #region Fields
        public static Services.WebsideDataConverter dataConverter;
        public static Business.User user;
        #endregion

        

        public static void SetMainPage() => Current.MainPage = new TabbedPage
        {
            Children =
                {
                    new NavigationPage(new LottoHomePage(dataConverter, user))
                    {
                        Title = "Auswerter",
                        Icon = Device.OnPlatform("tab_feed.png",null,null)
                    },
                    new NavigationPage(new LottoUserView(dataConverter, user))
                    {
                        Title = "User",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                    new NavigationPage(new AboutPage())
                    {
                        Title = "About",
                        Icon = Device.OnPlatform("tab_about.png",null,null)
                    },
                }
        };

        public static Page GetMainPage()
        {
            return new NavigationPage(new LottoHomePage());
        }
    }
}
