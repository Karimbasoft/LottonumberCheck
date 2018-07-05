using Android.Util;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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
        public AddLottoNumbers AddLottoNumbersPopUp { get; set; }
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
            Title = "User";
        }

        #region Methods
        private async Task AddSparkleBoxToUserListAsync()
        {
            AddLottoNumbersPopUp = new AddLottoNumbers(WebsideDataConverter, AppUser);
            AddLottoNumbersPopUp.Disappearing += AddLottoNumbersPopUp_Disappearing;
            await PopupNavigation.PushAsync(AddLottoNumbersPopUp);
        }

        private void AddLottoNumbersPopUp_Disappearing(object sender, EventArgs e)
        {
            if (AddLottoNumbersPopUp.Save)
            {
                if (AddLottoNumbersPopUp.SelectedNumbers.Count == 6)
                {
                    AppUser.AddEntryToSparkleBoxCollection(
                        SparkleBox.ConvertTicketNumberCollectionToString(AddLottoNumbersPopUp.SelectedNumbers));
                }
                AddLottoNumbersPopUp.Disappearing -= AddLottoNumbersPopUp_Disappearing;
                AddLottoNumbersPopUp.SelectedNumbers.Clear();
                }
        }

        private void DeleteSparkleBoxFromUserList(string numbers)
        {
            AppUser.DeleteEntryFromSparkleBox(numbers);
        }
        #endregion
    }
}
