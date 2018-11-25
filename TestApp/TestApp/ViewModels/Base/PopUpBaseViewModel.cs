using App.UI.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels.Base
{
    public class PopUpBaseViewModel : BaseViewModel
    {
        public ICommand CmdCloseWindow
        {
            get
            {
                return new Command((object obj) =>
               {
                   CloseWindow();
               });
            }
        }

        /// <summary>
        /// Close the PopUp Window
        /// </summary>
        private async void CloseWindow() => await PopupNavigation.PopAsync(true);
    }
}
