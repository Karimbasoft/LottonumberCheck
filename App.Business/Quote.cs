using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business
{
    public class Quote
    {
        #region Propertys
        public string Name { get; set; }

        public int Winner { get; set; }

        public string Money { get; set; }
        #endregion

        public Quote(string name, int winner, string money)
        {
            Name = name;
            Winner = winner;
            Money = money;
        }
    }
}
