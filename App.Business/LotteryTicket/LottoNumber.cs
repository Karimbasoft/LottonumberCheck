using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.LotteryTicket
{
    public class LottoNumber : Digit
    {
        public LottoNumber()
        {
            Number = 0;
        }

        public LottoNumber(int number)
        {
            Number = number;
        }

        public LottoNumber(string number)
        {
            Number =  Int32.TryParse(number, out int tmpNumber) ? tmpNumber : 0;
        }

        #region Propertys

        #endregion
    }
}
