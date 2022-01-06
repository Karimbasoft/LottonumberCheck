using App.Business;
using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Web
{
    public class LottoService
    {
        #region Fields

        #endregion

        #region Propertys
        /// <summary>
        /// Representiert die LottoWebseite
        /// </summary>
        public LottoWebsideProvider LottoWebside { get; private set; }

        /// <summary>
        /// Gibt die Superzahl zurück
        /// </summary>
        public int Supernumber => LottoWebside.SuperNumber;

        /// <summary>
        /// Die Gewinnzaheln
        /// </summary>
        public ObservableCollection<long> LottoNumbers => LottoWebside.WinningNumbers;

        /// <summary>
        /// Super 6 Zahlen
        /// </summary>
        public ObservableCollection<int> Super66Numbers => LottoWebside.SuperSechsNumbers;

        /// <summary>
        /// Spiel77 Zahlen
        /// </summary>
        public ObservableCollection<int> Spiel77Numbers => LottoWebside.SpielSiebenundsiebzigNumbers;

        public List<Quote> LottoQuotes => LottoWebside.WinningQuotesLotto;
        #endregion

        public LottoService(string url, string qoutenUrl = null)
        {
            LottoWebside = new LottoWebsideProvider(url);
            
        }

        #region Methods
        public async Task Initilize()
        {
            await LottoWebside.InitilizeWebsideProvider();
        }
        #endregion
    }
}
