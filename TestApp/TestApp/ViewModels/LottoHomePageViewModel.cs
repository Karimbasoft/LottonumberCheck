using App.Business;
using App.Services;
using App.UI.PopUp;
using App.UI.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace App.UI.ViewModels
{
    public class LottoHomePageViewModel : BaseViewModel
    {
        private INavigation _navigation;
        private RelayCommand _loadLottoViewCommand;
        private static NLog.Logger logger;

        #region ICommand
        public ICommand LoadLottoViewCommand
        {
            get
            {

                if (_loadLottoViewCommand == null)
                {
                    try
                    {

                        _loadLottoViewCommand = new RelayCommand(async p => await _navigation.PushAsync(new UI.Views.LottoZahlenView(WebsideDataConverter, AppUser)), p => true);
                    }
                    catch (Exception ex)
                    {
                        logger.Warn($"Fehler beim anzeigen des Lotto Dialogs: {ex}");
                    }

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
                    try
                    {

                        await _navigation.PushAsync(new UI.Views.AdditionalLottoGameView(WebsideDataConverter, AppUser));
                    }
                    catch (Exception ex)
                    {
                        logger.Warn($"Fehler beim anzeigen des SpielAndSuper Dialogs: {ex}");
                    }
                });
            }
        }

        public ICommand CmdShowInfoPage
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        await ShowAboutDialog();
                    }
                    catch (Exception ex)
                    {
                        Log.Warning("MyNotes", $"Fehler beim anzeigen des Info Dialogs: {ex}");
                        logger.Warn($"Fehler beim anzeigen des Info Dialogs: {ex}");
                    }
                });

            }
        }
        #endregion

        #region Propertys
        public WebsideDataConverter WebsideDataConverter { get; set; }

        public User AppUser { get; set; }

        public About AboutPopUP { get; set; }
        #endregion

        public LottoHomePageViewModel(INavigation navigation, WebsideDataConverter websideDataConverter, User user)
        {
            _navigation = navigation;
            WebsideDataConverter = websideDataConverter;
            AppUser = user;
            logger = NLog.LogManager.GetCurrentClassLogger();
        }

        public LottoHomePageViewModel(INavigation navigation)
        {
            _navigation = navigation;
        }

        #region Methods
        private async Task ShowAboutDialog()
        {
            AboutPopUP = new About();
            await PopupNavigation.PushAsync(AboutPopUP);
        }
        #endregion
    }

}
