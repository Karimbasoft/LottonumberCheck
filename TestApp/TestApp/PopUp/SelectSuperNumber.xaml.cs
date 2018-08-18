using App.UI.ViewModels;
using Rg.Plugins.Popup.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.UI.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SelectSuperNumber : PopupPage
    {
        #region Propertys
        public int SelectedSuperNumber
        {
            set
            {
                viewModel.SelectedNumber = value;
            }
            get
            {
                return viewModel.SelectedNumber;
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
       


        SelectSuperNumberViewModel viewModel;
        public SelectSuperNumber()
        {
            InitializeComponent();
            viewModel = new SelectSuperNumberViewModel();
            BindingContext = viewModel;
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