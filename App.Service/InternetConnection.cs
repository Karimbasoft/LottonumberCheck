using Android.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace App.Services
{
    public class InternetConnection
    {
        #region Fields
        private string _url;
        #endregion
        
        public InternetConnection(string url)
        {
            Url = url; 
        }



        #region Property
        public string Url
        {
            get
            {
                return _url;
            }

            set
            {
                _url = value;
            }
        }
        #endregion

        /// <summary>
        /// Prüft ob eine Internetverbindung zustande kommt
        /// </summary>
        /// <returns></returns>
        public bool CheckInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead("http://www.google.com"))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                Log.Error("LottoscheinAuswerter","Keine Internetverbindung möglich");
                return false;
            }
        }

        /// <summary>
        /// Prüft ob eine Internetverbindung zustande kommt
        /// </summary>
        /// <returns></returns>
        public bool CheckInternetConnection(string url)
        {
            try
            {
                using (var client = new WebClient())
                {
                    using (var stream = client.OpenRead(url))
                    {
                        return true;
                    }
                }
            }
            catch
            {
                Log.Error("LottoscheinAuswerter", "Keine Internetverbindung möglich");
                return false;
            }
        }

        /// <summary>
        /// Zeigt eine MessageBox an
        /// </summary>
        /// <param name="titel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task ShowInformationMassageAsync(string titel, string text)
        {
            await App.UI.App.Current.MainPage.DisplayAlert(titel, text, "OK");
        }
    }
}
