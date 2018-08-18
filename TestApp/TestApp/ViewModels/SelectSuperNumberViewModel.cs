using App.Business.LotteryTicket;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class SelectSuperNumberViewModel : BaseViewModel
    {
        #region Fields

        #endregion

        #region Propertys
        public bool Save { get; private set; }

        public int SelectedNumber { get; set; }

        public ObservableCollection<LottoNumber> PossibleSuperNumbersCollections { get; set; }
        #endregion

        #region ICommand
        public ICommand CmdSelectSuperNumber
        {
            get
            {
                return new Command((object obj) =>
                {
                    int? number = (obj as LottoNumber).Number;
                    Save = true;
                    SelectedNumber = number != null ? (int)number : 0;
                    CloseWindow();
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

        
        #endregion

        public SelectSuperNumberViewModel()
        {
            CreateListWithPossibleSuperNumbers();
            Save = false;
        }

        #region Methods
        private async void CloseWindow()
        {
            await PopupNavigation.PopAsync(true);
        }

        private void CreateListWithPossibleSuperNumbers()
        {
            PossibleSuperNumbersCollections = new ObservableCollection<LottoNumber>();
            for (int i = 0; i <= 9; i++)
            {
                PossibleSuperNumbersCollections.Add(new LottoNumber(i));
            }
        }
        #endregion

    }
}
