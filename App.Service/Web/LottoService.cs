using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App.Service.Web
{
    public class LottoService
    {
        #region Fields
        private readonly LottoWebsideProvider _lottoWebside;
        #endregion

        #region Propertys
        /// <summary>
        /// Gibt die Superzahl zurück
        /// </summary>
        public int Supernumber => _lottoWebside.SuperNumber;

        /// <summary>
        /// Die Gewinnzaheln
        /// </summary>
        public ObservableCollection<LottoNumber> LottoNumbers => _lottoWebside.WinningNumbers;

        /// <summary>
        /// Super 6 Zahlen
        /// </summary>
        public ObservableCollection<LottoNumber> Super66Numbers => _lottoWebside.SuperSechsNumbers;

        /// <summary>
        /// Spiel77 Zahlen
        /// </summary>
        public ObservableCollection<LottoNumber> Spiel77Numbers => _lottoWebside.SpielSiebenundsiebzigNumbers;

        #endregion

        public LottoService(string url, string qoutenUrl = null)
        {
            _lottoWebside = new LottoWebsideProvider(url);
        }

        #region Methods

        #endregion
    }
}
