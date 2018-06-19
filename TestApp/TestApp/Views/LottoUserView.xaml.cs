using App.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TestApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LottoUserView : ContentPage
    {
        LottoUserViewModel viewModel;

        public LottoUserView(WebsideDataConverter websideDataConverter, TestApp.Business.User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new LottoUserViewModel(websideDataConverter, user);
        }

        public LottoUserView()
        {
            InitializeComponent();
        }
    }
}