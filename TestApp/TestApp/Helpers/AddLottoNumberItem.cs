using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.UI.Helpers
{
    public class AddLottoNumberItem : LottoNumber
    {
        private bool _isSelected;

        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                OnPropertyChanged();
            }
        }

        public AddLottoNumberItem(int number)
        {
            Number = number; 
        }
    }
}
