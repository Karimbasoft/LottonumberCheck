
using System;
using System.Diagnostics;
using System.Windows.Input;
using TestApp.Business;
using TestApp.Helpers;
using TestApp.Views;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class LottoHomePageViewModel : BaseViewModel
    {
        private INavigation _navigation;

        #region ICommand
        public ICommand LoadLottoViewCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PushAsync(new LottoZahlenView(WebsideDataConverter, AppUser));
                });

            }
        }

        public ICommand LoadSpielAndSuperCommand { get; }
        #endregion

        #region Propertys
        public Services.WebsideDataConverter WebsideDataConverter { get; set; }

        public User AppUser { get; set; }
        #endregion

        public LottoHomePageViewModel(INavigation navigation)
        {
            _navigation = navigation;
            WebsideDataConverter = new Services.WebsideDataConverter();
        }
    }
 
}
