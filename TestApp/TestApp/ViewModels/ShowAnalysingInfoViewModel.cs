using App.Business;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class ShowAnalysingInfoViewModel
    {
        #region Fields
        private readonly TicketAnalyzer _ticketAnalyzer;
        #endregion

        #region Propertys
        public ObservableCollection<SparkleAnalysis> WinningAnaylsis { get; set; }
        #endregion

        #region Commands
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
        #endregion

        public ShowAnalysingInfoViewModel(ObservableCollection<SparkleAnalysis> ticketAnalyzer)
        {
            WinningAnaylsis = ticketAnalyzer;
        }

        private async void CloseWindow()
        {
            await PopupNavigation.PopAsync(true);
        }
    }
}
