using App.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using static App.Business.Web.CSSClasses;

namespace App.Business.Web
{
    public class Webside
    {
        #region Fields
        private static NLog.Logger _logger;
        #endregion

        /// <summary>
        /// URL of the Webside
        /// </summary>
        public string URL { get; set; }

        public Webside(string url)
        {
            URL = url;
            _logger = NLog.LogManager.GetLogger("logfile");
        }

        #region Method
        private async Task<string> GetHTMLWebRequestAsync()
        {
            var httpClient = new HttpClient { Timeout = TimeSpan.FromSeconds(15) };
            httpClient.BaseAddress = new Uri(URL);
            try
            {
                using (var httpResponse = await httpClient.GetAsync(URL))
                {
                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {
                        return await httpResponse.Content.ReadAsStringAsync();
                    }
                    else
                    {
                        Android.Util.Log.Warn("LottoAuswerter", $"Probleme beim Download. Abgeborchen Status: {httpResponse.StatusCode}");
                        return string.Empty;
                    }
                }
            }
            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                _logger.Warn($"Keine Internetverindung möglich: {ex}");
                Android.Util.Log.Error("LottoAuswerter", $"Probleme beim Download der DAten: {ex}");
                return string.Empty;
            }
        }

        public async Task<string> GetHTMLAsync()
        {
            return await GetHTMLWebRequestAsync();
        }

        /// <summary>
        /// Zeigt eine MessageBox an
        /// </summary>
        /// <param name="titel"></param>
        /// <param name="text"></param>
        /// <returns></returns>
        private async System.Threading.Tasks.Task ShowInformationMassageAsync(string titel, string text)
        {
            await Xamarin.Forms.Device.InvokeOnMainThreadAsync(async () =>
             {
                 await App.UI.App.Current?.MainPage?.DisplayAlert(titel, text, "OK");
             });
        }
        #endregion
    }
}
