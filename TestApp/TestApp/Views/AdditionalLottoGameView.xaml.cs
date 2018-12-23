using App.Business;
using App.Service.Web;
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

        public AdditionalLottoGameView(LottoService lottoService, User user)
        {
            InitializeComponent();
            BindingContext = ViewModel = new AdditionalLottoGameViewModel(lottoService, user);
        }

        #region Propertys
        public AdditionalLottoGameViewModel ViewModel { get; }
        #endregion
        
    }
}