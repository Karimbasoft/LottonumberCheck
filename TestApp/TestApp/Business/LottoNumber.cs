using System;
using System.Collections.Generic;
using System.Text;

namespace TestApp.Business
{
    public class LottoNumber
    {
        public LottoNumber()
        {
            Number = 0;
        }

        public LottoNumber(int number)
        {
            Number = number;
        }

        #region Propertys
        public int Number { get; set; }
        #endregion
    }
}
