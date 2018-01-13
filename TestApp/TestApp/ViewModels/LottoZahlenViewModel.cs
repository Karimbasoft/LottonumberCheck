using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class LottoZahlenViewModel : BaseViewModel, INotifyPropertyChanged
    {
        #region Fields
        private string superNumberColor;
        private string _showInfoTable;

        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private Propertys
        private Services.WebsideDataConverter WebsideDataConverter { get; set; }
        #endregion

        #region Public Propertys
        public ObservableCollection<Business.LottoNumber> CurrentLottoNumbers { get; set; }
        public Business.User AppUser { get; set; }
        public int SuperNumber { get; set; }
        public int CountHits { get; set; }
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


        public LottoZahlenViewModel(Services.WebsideDataConverter websideDataConverter, Business.User user)
        {
            WebsideDataConverter = websideDataConverter;
            SuperNumber = WebsideDataConverter.SuperNumber;
            SuperNumberColor = "Red";
            CountHits = 2;
            ShowInfoTable = "False";
            CurrentLottoNumbers = WebsideDataConverter.WinningNumbers;
            AppUser = user;
        }

        public LottoZahlenViewModel()
        {

        }

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
