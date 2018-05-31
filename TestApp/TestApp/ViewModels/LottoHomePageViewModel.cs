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
        private RelayCommand _loadLottoViewCommand;
        private RelayCommand _loadSpielAndSuperCommand;

        #region ICommand
        public ICommand LoadLottoViewCommand
        {
            get
            {
                if (_loadLottoViewCommand == null)
                {
                    _loadLottoViewCommand = new RelayCommand(async p => await _navigation.PushAsync(new LottoZahlenView(WebsideDataConverter, AppUser)), p => true);
                    //}
                    //    return new Command(async () =>
                    //{
                    //    await _navigation.PushAsync(new LottoZahlenView(WebsideDataConverter, AppUser));
                    //});
                    
                }
                return _loadLottoViewCommand;
            }
        }

            private void test()
        {

        }

        public ICommand LoadSpielAndSuperCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PushAsync(new AdditionalLottoGameView(WebsideDataConverter, AppUser));
                });
            }
        }
        #endregion

        #region Propertys
        public Services.WebsideDataConverter WebsideDataConverter { get; set; }

        public User AppUser { get; set; }
        #endregion

        public LottoHomePageViewModel(INavigation navigation, Services.WebsideDataConverter websideDataConverter, User user)
        {
            _navigation = navigation;
            WebsideDataConverter = websideDataConverter;
            AppUser = user;
        }

        public LottoHomePageViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }
    }
 
}
