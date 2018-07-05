using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestApp.Business
{
    public class User : SparkleBoxMocker
    {
        #region Propertys
        public ObservableCollection<LottoNumber> LottoTicketNumber { get; set; }

        public string PathToSpecialFolder { get; set; }

        public string NameOfJsonUserNumbers { get; set; }

        public string NameOfJsonTicketNumber { get; }

        public int SuperNumber { get; set; }

        public string FolderName { get; }
        #endregion

        public User()
        {
            UserNumbers = new ObservableCollection<SparkleBox>();
        }
    }
}
