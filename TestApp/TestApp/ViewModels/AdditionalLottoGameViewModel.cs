using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using TestApp.Business;
using TestApp.Services;

namespace App.UI.ViewModels
{
    public class AdditionalLottoGameViewModel : BaseViewModel
    {

        #region Fields

        #endregion

        #region Propertys
        public ObservableCollection<LottoNumber> UserSuperSechsNumbers { get; set; }
        public ObservableCollection<LottoNumber> UserSpielSiebenundsiebzigNumbers { get; set; }
        private User AppUser { get; }
        private WebsideDataConverter WebsideDataConverter { get; }
        #endregion

        public AdditionalLottoGameViewModel(WebsideDataConverter websideDataConverter, User user)
        {
            WebsideDataConverter = websideDataConverter;
            AppUser = user;
            UserSuperSechsNumbers = WebsideDataConverter.SuperSechsNumbers;
            UserSpielSiebenundsiebzigNumbers = WebsideDataConverter.SpielSiebenundsiebzigNumbers;
        }

        public AdditionalLottoGameViewModel()
        {

        }

      
    }
}
