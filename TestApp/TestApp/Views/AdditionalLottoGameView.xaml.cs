using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class AdditionalLottoGameView : ContentPage
	{
		public AdditionalLottoGameView ()
		{
			InitializeComponent();
		}

        public AdditionalLottoGameView(Services.WebsideDataConverter websideDataConverter, Business.User user)
        {
            InitializeComponent();
            BindingContext = ViewModel = new AdditionalLottoGameViewModel(websideDataConverter, user);
        }

        #region Propertys
        public AdditionalLottoGameViewModel ViewModel { get; }
        #endregion
        
    }
}