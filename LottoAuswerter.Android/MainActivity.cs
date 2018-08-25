using Android.App;
using Android.Widget;
using Android.Content.PM;
using App.UI;
using Android.OS;
using Android;

namespace LottoAuswerter.Android
{
    [Activity(Label = "LottoAuswerter.Android", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {

            base.OnCreate(savedInstanceState);
            Rg.Plugins.Popup.Popup.Init(this, savedInstanceState);
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App.UI.App());
        }
    }
}

