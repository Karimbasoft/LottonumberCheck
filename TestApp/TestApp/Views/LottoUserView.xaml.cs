using App.Business;
using App.Service.Web;
using App.Services;
using App.UI.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App.UI.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LottoUserView : ContentPage
    {
        #region Fields
        LottoUserViewModel viewModel;
        #endregion

        #region Propertys

        #endregion

        public LottoUserView(LottoService lottoService, User user)
        {
            InitializeComponent();
            BindingContext = viewModel = new LottoUserViewModel(lottoService, user);
        }

        public LottoUserView() => InitializeComponent();
    }
}