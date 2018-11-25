using App.Business.LotteryTicket;
using App.UI.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class SelectSuperNumberViewModel : PopUpBaseViewModel
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
        #endregion

        public SelectSuperNumberViewModel()
        {
            CreateListWithPossibleSuperNumbers();
            Save = false;
        }

        #region Methods
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
