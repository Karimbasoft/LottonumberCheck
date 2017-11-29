using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services;
using TestApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LottoZahlenView : ContentPage
	{
        LottoZahlenViewModel viewModel;
        private WebsideDataConverter websideDataConverter;

        public LottoZahlenView (WebsideDataConverter websideDataConverter)
		{
			InitializeComponent ();
            BindingContext = viewModel = new LottoZahlenViewModel(websideDataConverter);
        }

        public LottoZahlenView()
        {
            InitializeComponent();
        }
    }
}