using App.Business;
using App.Services;
using App.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.UI.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdditionalLottoGameView : ContentPage
	{
		public AdditionalLottoGameView ()
		{
			InitializeComponent();
		}

        public AdditionalLottoGameView(WebsideDataConverter websideDataConverter, User user)
        {
            InitializeComponent();
            BindingContext = ViewModel = new AdditionalLottoGameViewModel(websideDataConverter, user);
        }

        #region Propertys
        public AdditionalLottoGameViewModel ViewModel { get; }
        #endregion
        
    }
}