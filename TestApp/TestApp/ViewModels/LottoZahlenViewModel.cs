using App.Business;
using App.Business.LotteryTicket;
using App.Service.Web;
using App.Services;
using App.UI.Converter;
using App.UI.PopUp;
using Rg.Plugins.Popup.Services;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class LottoZahlenViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields
        private string superNumberColor;
        private string _showInfoTable;
        private int _countHits;
        private string _totalProfit;
        private readonly LottoService _lottoService;
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Public Propertys
        public ObservableCollection<LottoNumber> CurrentLottoNumbers { get; set; }
        private List<int> HitsInThePossibleProfitArea { get; set; }
        public  User AppUser { get; set; }
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
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Zeigt durch eine Farbe an, ob die Superzahl richtig war
        /// </summary>
        public string SuperNumberColor
        {
            get
            {
                return superNumberColor;
            }
            set
            {
                superNumberColor = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Gibt an, ob die InfoTabelle angezeigt wird oder nicht
        /// </summary>
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

        //Gibt den gesamten Gewinn zurück
        public string TotalProfit
        {
            get
            {
                return _totalProfit;
            }
            set
            {
                _totalProfit = value;
                NotifyPropertyChanged();
            }
        }

        /// <summary>
        /// Das PopUp zum auswählen der Superzahl
        /// </summary>
        public SelectSuperNumber SelectSuperNumberPopUp { get; set; }

        /// <summary>
        /// Das PopUp zur Ansicht der Gewinninformationen
        /// </summary>
        public ShowAnalysingInfo ShowAnalysingInfoPopUp { get; set; }
        #endregion

        #region Commands
        public ICommand ShowEvaluation
        {
            get
            {
                return new Command(async () =>
                {
                    await ShowSuperNumberPopUpAsync();  
                });

            }
        }

        public ICommand CmdShowMoreInformations
        {
            get
            {
                return new Command(async () =>
                {
                    await ShowWinningAnalysisPopUpAsync();
                });

            }
        }
        #endregion


        public LottoZahlenViewModel(LottoService lottoService,
            User user)
        {
            _lottoService = lottoService;
            SuperNumber = _lottoService.Supernumber;
            HitsInThePossibleProfitArea = new List<int>();
            WinningAnaylsis = new ObservableCollection<SparkleAnalysis>();
            SuperNumberColor = "White";
            ShowInfoTable = "False";
            CountHits = 0;         
            CurrentLottoNumbers = _lottoService.LottoNumbers;
            AppUser = user;
        }

        public LottoZahlenViewModel()
        {

        }


        #region Methods

        /// <summary>
        /// Zählt die Treffer des Benutzers und erstellt eine Analyse der Treffer (Gewinn, etc.)
        /// </summary>
        private void CountHitsFromUser()
        {
            int counter = 0;
            int biggestCounter = 0;
            List<int> UsernumbersFromSparkleBox;
            List<SparkleAnalysis> listWithAnalysis = new List<SparkleAnalysis>();
            WinningAnaylsis.Clear();

            foreach (SparkleBox sparkleBox in AppUser.UserNumbers)
            {
                counter = 0;
                UsernumbersFromSparkleBox = SparkleBoxConverter.ConvertSparkleBoxToIntList(sparkleBox);

                foreach (int number in UsernumbersFromSparkleBox)
                {
                    foreach (LottoNumber throwNumber in CurrentLottoNumbers)
                    {
                        if (throwNumber.Number == number)
                            counter++;
                    }
                }

                listWithAnalysis.Add(new SparkleAnalysis(sparkleBox, counter, "0.00 €"));

                if (counter >= 2)
                {
                    HitsInThePossibleProfitArea.Add(counter);
                    
                }

                if (counter > biggestCounter)
                {
                    CountHits = counter;
                    biggestCounter = counter;
                }
            }
            WinningAnaylsis = TicketAnalyzer.CreateWinningAnalysis(listWithAnalysis,
                TicketAnalyzer.CheckIfSupernumberIsAnHit(AppUser.SuperNumber, _lottoService.Supernumber),
                _lottoService.LottoQuotes.ToList());

            if (CountHits < 0)
            {
                CountHits = 0;
            }
        }

        private async Task ShowSuperNumberPopUpAsync()
        {
            SelectSuperNumberPopUp = new SelectSuperNumber();
            SelectSuperNumberPopUp.Disappearing += SelectSuperNumberPopUp_Disappearing;
            await PopupNavigation.PushAsync(SelectSuperNumberPopUp);
        }

        private async Task ShowWinningAnalysisPopUpAsync()
        {
            ShowAnalysingInfoPopUp = new ShowAnalysingInfo(WinningAnaylsis);
            ShowAnalysingInfoPopUp.Disappearing += ShowAnalysingInfoPopUp_Disappearing;
            await PopupNavigation.PushAsync(ShowAnalysingInfoPopUp);
        }

        private void ShowAnalysingInfoPopUp_Disappearing(object sender, EventArgs e)
        {
            ShowAnalysingInfoPopUp = null;
        }

        private void StartToCheckLottoTicket()
        {
            CompareSuperNumbers();
            CountHitsFromUser();
            CalcTheTotalAmount();
            ShowInfoTable = "True";
        }

        private void CalcTheTotalAmount()
        {
            double totalAmount = 0;

            foreach (var item in WinningAnaylsis)
            {
                totalAmount += TicketAnalyzer.ToDouble(item.AmountOfMoney);
            }

            TotalProfit = Business.Converter.MoneyConverter.DoubleToEuros(totalAmount);
        }

        /// <summary>
        /// Bestimmt die Farbausgabe der Superzahl, Grün = Treffer / Rot = Kein Treffer
        /// </summary>
        private void CompareSuperNumbers()
        {
            if (TicketAnalyzer.CheckIfSupernumberIsAnHit(AppUser.SuperNumber, _lottoService.Supernumber))
            {
                SuperNumberColor = "Green";
            }
            else
            {
                SuperNumberColor = "Red";
            }
        }

        /// <summary>
        /// Wird aufgerufen, sobald Popup geschlossen wurde
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SelectSuperNumberPopUp_Disappearing(object sender, EventArgs e)
        {
            if (SelectSuperNumberPopUp.Save)
            {
                AppUser.SuperNumber = SelectSuperNumberPopUp.SelectedSuperNumber;
                SelectSuperNumberPopUp.Disappearing -= SelectSuperNumberPopUp_Disappearing;
                SelectSuperNumberPopUp = null;
                StartToCheckLottoTicket();
            }
        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

    }
}
