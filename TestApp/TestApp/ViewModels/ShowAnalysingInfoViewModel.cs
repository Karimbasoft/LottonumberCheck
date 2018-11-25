using App.Business;
using App.UI.ViewModels.Base;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace App.UI.ViewModels
{
    public class ShowAnalysingInfoViewModel : PopUpBaseViewModel
    {
        #region Fields

        #endregion

        #region Propertys
        public ObservableCollection<SparkleAnalysis> WinningAnaylsis { get; set; }
        #endregion

        #region Commands

        #endregion

        public ShowAnalysingInfoViewModel(ObservableCollection<SparkleAnalysis> ticketAnalyzer)
        {
            WinningAnaylsis = ticketAnalyzer;
        }
    }
}
