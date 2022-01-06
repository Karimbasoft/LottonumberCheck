using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using App.Business.Base;

using Xamarin.Forms;

namespace App.UI.ViewModels
{
	public class BaseViewModel : ObservableObject, INotifyPropertyChanged
    {
		bool isBusy = false;
        public event PropertyChangedEventHandler PropertyChanged;


        public bool IsBusy
		{
			get { return isBusy; }
			set { SetProperty(ref isBusy, value); }
		}
		/// <summary>
		/// Private backing field to hold the title
		/// </summary>
		string title = string.Empty;
		/// <summary>
		/// Public property to set and get the title of the item
		/// </summary>
		public string Title
		{
			get { return title; }
			set { SetProperty(ref title, value); }
		}

        protected virtual void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

