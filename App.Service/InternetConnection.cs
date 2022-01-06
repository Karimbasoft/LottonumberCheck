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
        public static async Task<bool> CheckInternetConnectionAsync(string url = "http://www.google.com")
        {
            try
            {
                //using (var client = new HttpClient())
                //{
                //    return string.IsNullOrEmpty(await client?.GetStringAsync(url)); 
                //}
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("LottoAuswerter", $"Keine Internetverbindung möglich {ex}");
                return false;
            }
        }
    }
}
