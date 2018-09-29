using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Service
{
    public class LotteryTicketService
    {
        #region Fields
        
        #endregion
        
        #region Propertys
        public ObservableCollection<LotteryTicket> LotteryTicketCollection { get; private set; }
        #endregion

        public LotteryTicketService()
        {
            ObservableCollection<LotteryTicket> LotteryTicket = new ObservableCollection<LotteryTicket>();
        }

        #region Methods
        /// <summary>
        /// Fügt einen Lottoschein der Liste hinzu
        /// </summary>
        /// <param name="lotteryTicket"></param>
        /// <returns></returns>
        public async Task<bool> AddLotteryTicketToCollection(LotteryTicket lotteryTicket)
        {
            bool complete = false;

            if (lotteryTicket.SparkleBoxCollection != null &&
                lotteryTicket.SparkleBoxCollection.Count < 12 &&
                lotteryTicket.TicketNumber > 0)
            {
                LotteryTicketCollection.Add(lotteryTicket);
            }

            return complete;
        }

        /// <summary>
        /// Entfernt ein Lottoschein aus der Liste
        /// </summary>
        /// <param name="lotteryTicket"></param>
        /// <returns></returns>
        public async Task<bool> RemoveLotteryTicketToCollection(LotteryTicket lotteryTicket)
        {
            bool complete = false;

            if (LotteryTicketCollection.Any(l => l.TicketNumber == lotteryTicket.TicketNumber))
            {
                LotteryTicketCollection.Remove(
                    LotteryTicketCollection.FirstOrDefault(l => l.TicketNumber == lotteryTicket.TicketNumber));
                complete = true;
            }

            return complete;
        }

        /// <summary>
        /// Updated ein Lottoschein aus der Liste
        /// </summary>
        /// <param name="lotteryTicket"></param>
        /// <returns></returns>
        public async Task<bool> UpdateLotteryTicketToCollection(LotteryTicket lotteryTicket)
        {
            bool complete = false;

            if (LotteryTicketCollection.Any(l => l.TicketNumber == lotteryTicket.TicketNumber))
            {
                LotteryTicketCollection.Remove(
                    LotteryTicketCollection.FirstOrDefault(l => l.TicketNumber == lotteryTicket.TicketNumber));

                if(await AddLotteryTicketToCollection(lotteryTicket))
                {
                    complete = true;
                }   
            }
            return complete;
        }
        #endregion
    }
}
