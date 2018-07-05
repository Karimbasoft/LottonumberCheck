﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Animations.Base;
using Xamarin.Forms;
using App.UI.ViewModels;
using Xamarin.Forms.Xaml;
using TestApp.Services;
using System.Collections.ObjectModel;
using TestApp.Business;
using App.Business.LotteryTicket;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddLottoNumbers : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Propertys
        public ObservableCollection<LottoNumber> SelectedNumbers
        {
            get
            {
                return viewModel.SelectedLottoNumbersCollection;
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

        public AddLottoNumbers(WebsideDataConverter websideDataConverter, Business.User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new AddLottoNumbersViewModel(websideDataConverter, user);
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