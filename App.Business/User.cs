using App.Business.LotteryTicket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace App.Business
{
    public class User
    {
        public ObservableCollection<SparkleBox> UserNumbers { get; set; }

        [JsonIgnore]
        private SparkleBoxMocker _sparkleBoxMocker;

        #region Propertys
        public ObservableCollection<LottoNumber> LottoTicketNumber { get; set; }

        [JsonIgnore]
        public string PathToSpecialFolder { get; set; }

        public string NameOfJsonUserNumbers { get; set; }

        public string NameOfJsonTicketNumber { get; }

        public int SuperNumber { get; set; }

        public string FolderName { get; }
        #endregion

        public User()
        {
            _sparkleBoxMocker = new SparkleBoxMocker(this);
            SuperNumber = -1;
        }

        public User FromJSON()
        {
            return _sparkleBoxMocker.DeserializeUser();
        }

        public void AddEntryToSparkleBoxCollection(string numbers)
        {
            _sparkleBoxMocker.AddEntryToSparkleBoxCollection(numbers);
        }

        public void DeleteEntryFromSparkleBox(string numbers)
        {
            _sparkleBoxMocker.DeleteEntryFromSparkleBox(numbers);
        }
    }
}
