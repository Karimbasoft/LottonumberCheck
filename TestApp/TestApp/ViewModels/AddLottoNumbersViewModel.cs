﻿using App.Business;
using App.Business.LotteryTicket;
using App.Service.Web;
using App.Services;
using App.UI.Helpers;
using App.UI.ViewModels.Base;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class AddLottoNumbersViewModel : PopUpBaseViewModel
    {
        #region Fields
        private LottoService lottoService;
        private User user;
        #endregion

        #region Propertys
        public ObservableCollection<Helpers.AddLottoNumberItem> PossibleLottoNumberCollections { get; set; }
        public ObservableCollection<Helpers.AddLottoNumberItem> SelectedLottoNumbersCollection { get; set; }

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
                    Helpers.AddLottoNumberItem number = obj as Helpers.AddLottoNumberItem;

                    if (SelectedLottoNumbersCollection.Count < 6 
                        && !SelectedLottoNumbersCollection.Any(x => x.Number == number.Number))
                    {
                        number.IsSelected = true;
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
                    AddLottoNumberItem number = obj as AddLottoNumberItem;
                    number.IsSelected = false;
                    SelectedLottoNumbersCollection.Remove(number);           
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

        public AddLottoNumbersViewModel(LottoService pLottoService, User pUser)
        {
            PossibleLottoNumberCollections = CreatePossibleLottoNumberCollection();
            SelectedLottoNumbersCollection = new ObservableCollection<Helpers.AddLottoNumberItem>();
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
        private ObservableCollection<AddLottoNumberItem> CreatePossibleLottoNumberCollection()
        {
            ObservableCollection<AddLottoNumberItem> tmpLottoNumberCollection = new ObservableCollection<AddLottoNumberItem>();

            for(int i = 1; i <= 49; i++)
            {
                tmpLottoNumberCollection.Add(new AddLottoNumberItem(i));
            }

            return tmpLottoNumberCollection;
        }
    }
}
