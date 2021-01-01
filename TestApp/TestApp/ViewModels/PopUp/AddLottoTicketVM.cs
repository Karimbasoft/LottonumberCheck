using App.Business.LotteryTicket;
using App.UI.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels.PopUp
{
    public class AddLottoTicketVM : PopUpBaseViewModel
    {
        /// <summary>
        /// Zahlen des LottoTickets
        /// </summary>
        public ObservableCollection<LottoNumber> TicketNumbers { get; set; }

        /// <summary>
        /// Gibt an, ob der Eintrag gespeichert werden soll
        /// </summary>
        public bool Save { get; set; }

        #region Commands
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
        

        public AddLottoTicketVM()
        {
            TicketNumbers = new ObservableCollection<LottoNumber>();
            FillTicketNumbersWithTempNumbers();
        }

        private void FillTicketNumbersWithTempNumbers()
        {
            for (int i = 0; i < 1; i++)
            {
                TicketNumbers.Add(new LottoNumber(i));
            }
            
        }
    }
}
