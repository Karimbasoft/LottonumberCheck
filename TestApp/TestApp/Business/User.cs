using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace TestApp.Business
{
    public class User
    {
        #region Propertys
        public ObservableCollection<SparkleBox> UserNumbers { get; set; }
        public ObservableCollection<LottoNumber> LottoTicketNumber { get; set; }

        public string PathToSpecialFolder { get; set; }

        public string NameOfJsonUserNumbers { get; set; }

        public string NameOfJsonTicketNumber { get; }

        public int SuperNumber { get; set; }

        public string FolderName { get; }
        #endregion

        public User()
        {

        }
    }
}
