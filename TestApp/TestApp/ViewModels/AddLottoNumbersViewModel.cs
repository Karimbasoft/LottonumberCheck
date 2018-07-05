using App.Business.LotteryTicket;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using TestApp.Business;
using TestApp.Services;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class AddLottoNumbersViewModel : BaseViewModel
    {
        #region Fields
        private WebsideDataConverter websideDataConverter;
        private User user;
        #endregion

        #region Propertys
        public ObservableCollection<LottoNumber> PossibleLottoNumberCollections { get; set; }
        public ObservableCollection<LottoNumber> SelectedLottoNumbersCollection { get; set; }

        //Hat Benutzer den Speichern Button gedrückt
        public bool Save { get; set; }
        #endregion

        #region ICommand
        public ICommand CmdSelectLottoNumber
        {
            get
            {
                return new Command((object obj) =>
                {
                    LottoNumber number = obj as LottoNumber;

                    if (SelectedLottoNumbersCollection.Count < 6 
                        && !SelectedLottoNumbersCollection.Any(x => x.Number == number.Number))
                    {
                        SelectedLottoNumbersCollection.Add(number);
                    }             
                });
            }
        }

        public ICommand CmdRemoveSelectedNumber
        {
            get
            {
                return new Command((object obj) =>
                {
                    LottoNumber number = obj as LottoNumber;
                    SelectedLottoNumbersCollection.Remove(number);                   
                });
            }
        }

        public ICommand CmdCloseWindow
        {
            get
            {
                return new Command((object obj) =>
                {
                    Save = false;
                    CloseWindow();
                });
            }
        }

        public ICommand CmdSaveSelection
        {
            get
            {
                return new Command((object obj) =>
                {
                    Save = true;
                    CloseWindow();
                });
            }
        }
        #endregion

        public AddLottoNumbersViewModel(WebsideDataConverter pWebsideDataConverter, User pUser)
        {
            PossibleLottoNumberCollections = CreatePossibleLottoNumberCollection();
            SelectedLottoNumbersCollection = new ObservableCollection<LottoNumber>();
            Save = false;
        }

        public AddLottoNumbersViewModel()
        {

        }

        /// <summary>
        /// Erstellt eine Lottozahlen Sammlung mit den Zahlen von 1 - 49
        /// </summary>
        /// <param name="lottoNumberCollection"></param>
        /// <returns></returns>
        private ObservableCollection<LottoNumber> CreatePossibleLottoNumberCollection()
        {
            ObservableCollection<LottoNumber> tmpLottoNumberCollection = new ObservableCollection<LottoNumber>();

            for(int i = 1; i <= 49; i++)
            {
                tmpLottoNumberCollection.Add(new LottoNumber(i));
            }

            return tmpLottoNumberCollection;
        }

        private async void CloseWindow()
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}
