using App.Business;
using App.Services;
using App.UI.Views;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class LottoHomePageViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private RelayCommand _loadLottoViewCommand;

        #region ICommand
        public ICommand LoadLottoViewCommand
        {
            get
            {
                if (_loadLottoViewCommand == null)
                {
                    _loadLottoViewCommand = new RelayCommand(async p => await _navigation.PushAsync(new UI.Views.LottoZahlenView(WebsideDataConverter, AppUser)), p => true);
                    
                }
                return _loadLottoViewCommand;
            }
        }

        public ICommand LoadSpielAndSuperCommand
        {
            get
            {
                return new Command(async () =>
                {
                    await _navigation.PushAsync(new UI.Views.AdditionalLottoGameView(WebsideDataConverter, AppUser));
                });
            }
        }
        #endregion

        #region Propertys
        public WebsideDataConverter WebsideDataConverter { get; set; }

        public User AppUser { get; set; }
        #endregion

        public LottoHomePageViewModel(INavigation navigation, WebsideDataConverter websideDataConverter, User user)
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
