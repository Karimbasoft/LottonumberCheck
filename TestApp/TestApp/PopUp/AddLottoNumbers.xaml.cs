using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Animations.Base;
using Xamarin.Forms;
using App.UI.ViewModels;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using App.Business.LotteryTicket;
using App.Services;
using App.Business;
using App.Service.Web;

namespace App.UI.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLottoNumbers : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Propertys
        public ObservableCollection<LottoNumber> SelectedNumbers
        {
            get
            {
                var tmpCollection = new ObservableCollection<LottoNumber>();
                viewModel.SelectedLottoNumbersCollection.ToList().ForEach(x => tmpCollection.Add(new LottoNumber(x.Number)));

                return tmpCollection;
            }
        }

        public bool Save
        {
            get
            {
                return viewModel.Save;
            }
        }
        #endregion
       
        AddLottoNumbersViewModel viewModel;

        public AddLottoNumbers(LottoService lottoService, User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new AddLottoNumbersViewModel(lottoService, user);
        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }
    }
}