using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using TestApp.Business;
using TestApp.Services;
using Xamarin.Forms;

namespace TestApp.ViewModels
{
    public class LottoUserViewModel : BaseViewModel
    {
        #region Fields

        #endregion 

        #region Propertys
        public ObservableCollection<SparkleBox> UserLottoNumbers { get; set; }
        private User AppUser { get; }
        private WebsideDataConverter WebsideDataConverter { get; }
        #endregion

        #region Commands    
        public ICommand DeleteSparkleBox
        {
            get
            {
                return new Command((object obj) =>
                {
                    SparkleBox a = obj as SparkleBox;
                    DeleteSparkleBoxFromUserList(a.SparkleBoxNumbers);
                });
            }
        }

            
        public ICommand AddSparkleBox
        {
            get
            {
                return new Command(() =>
                {
                    AddSparkleBoxToUserList();
                });

            }
        }
        #endregion

        public LottoUserViewModel()
        {
            
        }

        public LottoUserViewModel(WebsideDataConverter websideDataConverter, User user)
        {
            WebsideDataConverter = websideDataConverter;
            AppUser = user;
            //AppUser.UserNumbers.Add(new SparkleBox(1, 13, 23, 33, 45, 46));
            UserLottoNumbers = AppUser.UserNumbers;
            //UserLottoNumbers.Add(new SparkleBox(1, 12, 22, 33, 44, 45));
        }

        #region Methods
        private void AddSparkleBoxToUserList()
        {
            AppUser.AddEntryToSparkleBoxCollection("1 23 24 34 45 55");
        }

        private void DeleteSparkleBoxFromUserList(string numbers)
        {
            AppUser.DeleteEntryFromSparkleBox(numbers);
        }
        #endregion
    }
}
