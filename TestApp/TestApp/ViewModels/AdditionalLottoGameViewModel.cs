using App.Business;
using App.Business.LotteryTicket;
using App.Service.Web;
using App.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace App.UI.ViewModels
{
    public class AdditionalLottoGameViewModel : BaseViewModel
    {

        #region Fields
        private readonly LottoService _lottoService;
        #endregion

        #region Propertys
        public ObservableCollection<int> UserSuperSechsNumbers { get; set; }
        public ObservableCollection<int> UserSpielSiebenundsiebzigNumbers { get; set; }
        private User AppUser { get; } 
        #endregion

        public AdditionalLottoGameViewModel(LottoService lottoService, User user)
        {
            _lottoService = lottoService;
            AppUser = user;
            UserSuperSechsNumbers = _lottoService.Super66Numbers;
            UserSpielSiebenundsiebzigNumbers = _lottoService.Spiel77Numbers;
        }

        public AdditionalLottoGameViewModel()
        {

        }

      
    }
}
