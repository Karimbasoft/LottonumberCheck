
using App.UI.ViewModels;
using Xamarin.Forms;

namespace App.UI.Views
{
	public partial class AboutPage : ContentPage
	{
        #region Fields
        private AboutViewModel aboutViewModel;
        #endregion

        public AboutPage()
		{
			InitializeComponent();
            BindingContext = aboutViewModel = new AboutViewModel();
		}
	}
}
