using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TestApp.Business;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class LottoZahlenViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields
        private string superNumberColor;
        private string _showInfoTable;
        private int _countHits;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Propertys
        private TestApp.Services.WebsideDataConverter WebsideDataConverter { get; set; }
        #endregion

        #region Public Propertys
        public ObservableCollection<TestApp.Business.LottoNumber> CurrentLottoNumbers { get; set; }
        private List<int> HitsInThePossibleProfitArea { get; set; }
        public TestApp.Business.User AppUser { get; set; }
        public int SuperNumber { get; set; }

        /// <summary>
        /// Beeinhaltet alle Auswertungen des Lottoscheins
        /// </summary>
        public ObservableCollection<SparkleAnalysis> WinningAnaylsis { get; set; }

        /// <summary>
        /// Die Treffer des Users
        /// </summary>
        public int CountHits
        {
            get
            {
                return _countHits;
            }
            set
            {
                _countHits = value;
                OnPropertyChanged();
            }
        }
        public string SuperNumberColor
        {
            get
            {
                return superNumberColor;
            }
            set
            {
                superNumberColor = value;
                OnPropertyChanged();
            }
        }
        public string ShowInfoTable
        {
            get
            {
                return _showInfoTable;
            }
            set
            {
                _showInfoTable = value;
                NotifyPropertyChanged();
            }
        }
        #endregion

        #region Commands
        public ICommand ShowEvaluation
        {
            get
            {
                return new Command(() =>
                {
                    ShowInfoTable = "True";
                });

            }
        }
        #endregion


        public LottoZahlenViewModel(TestApp.Services.WebsideDataConverter websideDataConverter, 
            TestApp.Business.User user)
        {
            WebsideDataConverter = websideDataConverter;
            SuperNumber = WebsideDataConverter.SuperNumber;
            HitsInThePossibleProfitArea = new List<int>();
            SuperNumberColor = "Red";
            ShowInfoTable = "False";
            CountHits = 0;         
            CurrentLottoNumbers = WebsideDataConverter.WinningNumbers;
            AppUser = user;
        }

        public LottoZahlenViewModel()
        {

        }


        #region Methods

        //private void CountHitsFromUser()
        //{
        //    int counter = 0;
        //    int biggestCounter = 0;
        //    List<int> UsernumbersFromSparkleBox;
        //    List<SparkleAnalysis> listWithAnalysis = new List<SparkleAnalysis>();
        //    WinningAnaylsis.Clear();

        //    foreach (SparkleBox sparkleBox in AppUser.UserNumbers)
        //    {
        //        counter = 0;
        //        UsernumbersFromSparkleBox = SparkleBoxConverter.ConvertSparkleBoxToIntList(sparkleBox);
        //        foreach (int number in UsernumbersFromSparkleBox)
        //        {
        //            foreach (LottoNumber throwNumber in CurrentLottoNumbers)
        //            {
        //                if (throwNumber.Number == number)
        //                    counter++;
        //            }
        //        }
        //        listWithAnalysis.Add(new SparkleAnalysis(sparkleBox, counter, ""));
        //        if (counter >= 2)
        //        {
        //            HitsInThePossibleProfitArea.Add(counter);
        //            if (counter > biggestCounter)
        //            {
        //                CountHits = counter;
        //                biggestCounter = counter;
        //            }
        //        }
        //    }
        //    WinningAnaylsis = TicketAnalyzer.CreateWinningAnalysis(listWithAnalysis);

        //    if (CountHits < 0 )
        //    {
        //        CountHits = 0;
        //    }
        //}


        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        
    }
}
