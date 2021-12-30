using Android.Util;
using App.Business;
using App.Business.LotteryTicket;
using App.Service.Web;
using App.Services;
using App.UI.PopUp;
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
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class LottoUserViewModel : BaseViewModel
    {
        #region Fields
        private LottoService _lottoService { get; }
        private int _deleteClickCounter = 0;
        private SelectSuperNumber _selectSuperNumber;
        #endregion 

        #region Propertys
        public ObservableCollection<SparkleBox> UserLottoNumbers { get; set; }
        private User AppUser { get; }
        
        public AddLottoNumbers AddLottoNumbersPopUp { get; set; }

        /// <summary>
        /// PopUp für das hinzufügen von Lottozahlen
        /// </summary>
        public AddLottoTicket AddLottoTicketPopUp { get; set; }

        public bool IsListEmpty
        {
            get
            {
                return UserLottoNumbers.Count > 0 ? false : true;
            }
        }

        public int SuperNumber => AppUser.SuperNumber;
        #endregion

        #region Commands    
        public ICommand DeleteSparkleBox
        {
            get
            {
                return new Command((object obj) =>
                {
                    if (obj is SparkleBox a)
                    {
                        DeleteSparkleBoxFromUserList(a.SparkleBoxNumbers);
                    }
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

        public ICommand ClearSparkleBox
        {
            get
            {
                return new Command(() =>
                {
                    _deleteClickCounter += 1;

                    if (_deleteClickCounter == 2)
                    {
                        AppUser.UserNumbers.Clear();
                    }  
                });

            }
        }

        public ICommand ChangeTicketNumber
        {
            get
            {
                return new Command(async (object obj) =>
                {
                    await AddLottoTicketAsync();
                });

            }
        }

        public ICommand SelectSuperNumber
        {
            get
            {
                return new Command(async (object obj) =>
                {
                    _selectSuperNumber = new SelectSuperNumber();
                    _selectSuperNumber.Disappearing += _selectSuperNumber_Disappearing; ;
                    await PopupNavigation.Instance.PushAsync(_selectSuperNumber);
                });

            }
        }

        


        #endregion

        public LottoUserViewModel()
        {
            
        }

        public LottoUserViewModel(LottoService lottoService, User user)
        {
            _lottoService = lottoService;
            AppUser = user;
            UserLottoNumbers = AppUser.UserNumbers;
            Title = "User";

            //if (!UserLottoNumbers.Any())
            //{
            //    UserLottoNumbers.Add(new SparkleBox(1, 2, 3, 4, 5, 6));
            //}
        }

        #region Methods
        private async Task AddSparkleBoxToUserListAsync()
        {
            AddLottoNumbersPopUp = new AddLottoNumbers(_lottoService, AppUser);
            AddLottoNumbersPopUp.Disappearing += AddLottoNumbersPopUp_Disappearing;
            await PopupNavigation.Instance.PushAsync(AddLottoNumbersPopUp);
        }

        /// <summary>
        /// Öffnet das PopUp für das hinzufügen eines Tickets
        /// </summary>
        /// <returns></returns>
        private async Task AddLottoTicketAsync()
        {
            AddLottoTicketPopUp = new AddLottoTicket();
            AddLottoTicketPopUp.Disappearing += AddLottoTicketPopUp_Disappearing; 
            await PopupNavigation.PushAsync(AddLottoTicketPopUp);
        }

        private void AddLottoTicketPopUp_Disappearing(object sender, EventArgs e)
        {
            if (AddLottoNumbersPopUp != null && AddLottoNumbersPopUp.Save)
            {

            }
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

        private void _selectSuperNumber_Disappearing(object sender, EventArgs e)
        {
            if (sender is SelectSuperNumber superNumberDialog)
            {
                if (superNumberDialog.Save)
                {
                    AppUser.SuperNumber = superNumberDialog.SelectedSuperNumber;
                    OnPropertyChanged(nameof(SuperNumber));
                }
            }
        }

        private void DeleteSparkleBoxFromUserList(string numbers)
        {
            AppUser.DeleteEntryFromSparkleBox(numbers);
        }
        #endregion
    }
}
