using Rg.Plugins.Popup.Services;
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
using TestApp.Views;
using Xamarin.Forms;

namespace App.UI.ViewModels
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
                return new Command(async () =>
                {
                    await AddSparkleBoxToUserListAsync();
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
            UserLottoNumbers = AppUser.UserNumbers;
            //UserLottoNumbers.Add(new SparkleBox(1, 12, 22, 33, 44, 45));
        }

        #region Methods
        private async System.Threading.Tasks.Task AddSparkleBoxToUserListAsync()
        {
            await PopupNavigation.PushAsync(new AddLottoNumbers());
            //AppUser.AddEntryToSparkleBoxCollection("1 23 24 34 45 55");
        }

        private void DeleteSparkleBoxFromUserList(string numbers)
        {
            AppUser.DeleteEntryFromSparkleBox(numbers);
        }
        #endregion
    }
}
