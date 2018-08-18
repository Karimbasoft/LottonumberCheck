using App.Business;
using App.UI.ViewModels;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.UI.PopUp
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowAnalysingInfo : Rg.Plugins.Popup.Pages.PopupPage
    {
        #region Fields
        ShowAnalysingInfoViewModel viewModel;
        #endregion

        public ShowAnalysingInfo(ObservableCollection<SparkleAnalysis> sparkleAnalyser)
        {
            InitializeComponent();
            BindingContext = viewModel = new ShowAnalysingInfoViewModel(sparkleAnalyser);
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