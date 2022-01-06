using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace App.Service.Notification
{
    public static class NotificationService
    {
        public static async Task ShowMessageBoxAsync(string titel, string text)
        {
          await  Xamarin.Forms.Device.InvokeOnMainThreadAsync(async () =>
          {
               await App.UI.App.Current.MainPage.DisplayAlert(titel, text, "OK");
          });
        }
    }
}
