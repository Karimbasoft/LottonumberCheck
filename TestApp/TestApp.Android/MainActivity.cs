using Android.App;
using Android.Content.PM;
using Android.OS;
using UI = App.UI;
using System;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Rg.Plugins.Popup.Droid;

namespace TestApp.Droid
{
    [Activity(Label = "TestApp.Android", Theme = "@style/MyTheme", MainLauncher = false, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {



        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);
            //Rg.Plugins.Popup.Popup.Init(this, bundle);
            

            global::Xamarin.Forms.Forms.Init(this, bundle);

            LoadApplication(new UI.App());
        }
    }
}