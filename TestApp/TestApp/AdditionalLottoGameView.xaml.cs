using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp
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
            BindingContext = viewModel = new AdditionalLottoGameViewModel(websideDataConverter, user);
        }

        #region Propertys
        public AdditionalLottoGameViewModel viewModel { get; }
        #endregion
        
    }
}