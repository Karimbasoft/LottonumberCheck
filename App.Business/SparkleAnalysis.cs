using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business
{
    public class SparkleAnalysis
    {
        #region Propertys

        /// <summary>
        /// Lottozahlen
        /// </summary>
        public SparkleBox Lottozahlen { get; set; }

        /// <summary>
        /// Treffer
        /// </summary>
        public int Hits { get; set; }

        /// <summary>
        /// Geldbetrag
        /// </summary>
        public string AmountOfMoney { get; set; }
        #endregion

        public SparkleAnalysis()
        {

        }

        public SparkleAnalysis(SparkleBox lottozahlen, int hits, string amountOfMoney)
        {
            Lottozahlen = lottozahlen;
            Hits = hits;
            AmountOfMoney = amountOfMoney;
        }
    }
}
