using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace TestApp.Services
{
    public class InternetConnection
    {
        #region Fields
        private string _url;
        private string _htmlQuellcode;
        private bool _isSynchronMode = true;
        #endregion
        
        public InternetConnection(string url)
        {
            Url = url;

            if (_isSynchronMode)
            {
                HtmlQuellcode = GetHTMLWebRequest();
            }
            else
            {
                StartToDownloadHTMLSourceAsync(60);
            }          
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

        public string HtmlQuellcode
        {
            get
            {
                return _htmlQuellcode;
            }

            set
            {
                _htmlQuellcode = value;
            }
        }

        #endregion

        public async void StartToDownloadHTMLSourceAsync(int timeInSeconds)
        {
             HtmlQuellcode = await GetHTMLAsync(timeInSeconds);
        }

        public async Task<string> GetHTMLAsync(int timeInSeconds)//http://www.lotto24.de/webshop/product/lottonormal/result
        {
            string html = "";
            CancellationTokenSource tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(timeInSeconds));
            CancellationToken token = tokenSource.Token;

            await Task.Run(async () =>
            {

                 if (CheckInternetConnection())
                 {
                     html = await Task.FromResult(GetHTMLWebRequest());
                 }
                 else
                 {
                     html = "";
                     await ShowInformationMassageAsync("No connection", "Es ist nicht möglich eine Verbindung zum Internet herzustellen !");
                 }
             }, token);


            if (token.IsCancellationRequested)
            {
                HtmlQuellcode = "";
                Debugger.Break();
            }

            //Quellcode zurückgeben
            return html;
        }

        private string GetHTMLWebRequest()
        {
            string html = "";
            try
            {
                string url = Url;


                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.ContentType = "text/html";
                HttpWebResponse myResp = (HttpWebResponse)request.GetResponse();

                using (var response = request.GetResponse())
                {
                    using (var reader = new StreamReader(response.GetResponseStream()))
                    {
                        html = reader.ReadToEnd();
                    }
                }
            }

            catch (WebException exception)
            {
                Console.WriteLine(exception.Message);
                html = "";
            }
            return html;
        }

        /// <summary>
        /// Prüft ob eine Internetverbindung zustande kommt
        /// </summary>
        /// <returns></returns>
        private bool CheckInternetConnection()
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
            await App.Current.MainPage.DisplayAlert(titel, text, "OK");
        }
    }
}
