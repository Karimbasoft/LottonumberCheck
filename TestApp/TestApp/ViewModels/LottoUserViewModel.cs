using System;
using System.Collections.Generic;
using System.Text;
using TestApp.Business;
using TestApp.Services;

namespace TestApp.ViewModels
{
    public class LottoUserViewModel
    {
        private WebsideDataConverter websideDataConverter;
        private User user;

        #region Propertys
        #endregion


        public LottoUserViewModel()
        {
            
        }

        public LottoUserViewModel(WebsideDataConverter websideDataConverter, User user)
        {
            this.websideDataConverter = websideDataConverter;
            this.user = user;
        }
    }
}
