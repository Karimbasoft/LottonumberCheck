using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace App.Business.LotteryTicket
{
    public class LotteryTicket
    {
        #region Propertys
        /// <summary>
        /// The TicketNumber
        /// </summary>
        public int TicketNumber { get; set; }

        /// <summary>
        /// Collection of SparkleBoxes in the LotteryTicket
        /// </summary>
        public ObservableCollection<SparkleBox> SparkleBoxCollection { get; set; }

        #endregion


        public LotteryTicket(int number)
        {
            SparkleBoxCollection = new ObservableCollection<SparkleBox>();
            TicketNumber = number;
        }

        public LotteryTicket(ObservableCollection<SparkleBox> sparkleBoxCollection)
        {
            SparkleBoxCollection = sparkleBoxCollection;
        }

        #region Methods
        public async Task<bool> AddSparkleBoxToLotteryTicket(SparkleBox sparkleBox)
        {
            bool complete = false;

            if (SparkleBoxCollection != null &&
                SparkleBoxCollection.Count < 12 &&
                sparkleBox.SparkleBoxNumbers.Length == 6)
            {
                SparkleBoxCollection.Add(sparkleBox);
            }

            return complete;
        }
        #endregion
    }
}
