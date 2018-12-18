using App.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
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
        private InternetConnection _connection;
        private bool _isSynchronMode = true;
        private static NLog.Logger _logger;
        #endregion

        /// <summary>
        /// URL of the Webside
        /// </summary>
        public string URL { get; set; }

        /// <summary>
        /// The HTML Source Code of the Webside
        /// </summary>
        public string HTMLCode
        {
            get
            {
                return GetHTMLWebRequest();
            }
        }

        /// <summary>
        /// Webide Online
        /// </summary>
        public bool Online
        {
            get
            {
                return _connection.CheckInternetConnection(URL);
            }
        }

        /// <summary>
        /// Beinhaltet die CSSClassen
        /// </summary>
        public Dictionary<string, string> CSSClassDictionary { get; set; }

        public Webside(string url)
        {
            URL = url;
            _connection = new InternetConnection(URL);
            CSSClassDictionary = CreateStandardCSSClasses();
            _logger = NLog.LogManager.GetLogger("logfile");
        }

        #region Method
        private string GetHTMLWebRequest()
        {
            string html = "";
            try
            {
                if (_connection.CheckInternetConnection(URL))
                {

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(URL);
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
                else
                {
                    ShowInformationMassageAsync("No connection", "Es ist nicht möglich eine Verbindung zum Internet herzustellen !");
                    _logger.Warn("Keine Internetverindung möglich !");
                }
            }

            catch (WebException ex)
            {
                Console.WriteLine(ex.Message);
                html = "";
                _logger.Warn($"Keine Internetverindung möglich: {ex}");
            }
            return html;
        }

        public async Task<string> StartToDownloadHTMLSourceAsync(int timeInSeconds)
        {
            return await GetHTMLAsync(timeInSeconds);
        }

        public async Task<string> GetHTMLAsync(int timeInSeconds)//http://www.lotto24.de/webshop/product/lottonormal/result
        {
            string html = "";
            CancellationTokenSource tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(timeInSeconds));
            CancellationToken token = tokenSource.Token;

            await Task.Run(async () =>
            {

                if (_connection.CheckInternetConnection(URL))
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
                html = "";
                Debugger.Break();
            }

            //Quellcode zurückgeben
            return html;
        }

        //private Dictionary<string, string> CreateStandardCSSClasses()
        //{
        //    Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
        //    {
        //        //{ CSSClassNames.SuperNumber.ToString(), "class=\"winning-numbers__number winning-numbers__number--superzahl\"" },
        //        //{ CSSClassNames.WinningQuotesSpielSiebenundsiebzigStart.ToString(), "class=\"inner-table-header align-middle hidden-xs" },
        //        //{ CSSClassNames.WinningQuotesSpielSiebenundsiebzigEnd.ToString(),  "class=\"inner-table-header align-middle hidden-xs" },
        //        //{ CSSClassNames.WinningQuotesLottoStart.ToString(),  "class=\"inner-table-header align-middle hidden-xs" },
        //        //{ CSSClassNames.WinningQuotesLottoEnd.ToString(),   "class=\"inner-table-header align-middle visible-xs-block" }
        //        {CSSClassNames.LottoNumberStart.ToString(), "class=\"product__box__content__result__numbers\"" },
        //        {CSSClassNames.LottoNumberEnd.ToString(), "class=\"product__box__content__result__numbers--square\"" },
        //        {CSSClassNames.AdditionalLottoGameStart.ToString(), "class=\"product__box__content__result__numbers--square\"" },
        //        {CSSClassNames.AdditionalLottoGameEnd.ToString(), "class=\"confirm middle further--arrow quota hidden-xs\"" },
        //        {CSSClassNames.WinningQuotesLottoStart.ToString(), "class=\"col-xs-5 col-sm-4\" data-test-id=\"winningNumbersCol\"" },
        //        {CSSClassNames.WinningQuotesLottoEnd.ToString(), "class=\"info-box alert-box jackpot-box\"" }
        //    };


        //    return keyValuePairs;
        //}

        private Dictionary<string, string> CreateStandardCSSClasses()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                {CSSClassNames.LottoNumberStart.ToString(), "class=\"draw-results\"" },
                {CSSClassNames.LottoNumberEnd.ToString(), "class=\"zusatzlotterien\"" },
                {CSSClassNames.AdditionalLottoGameStart.ToString(), "class=\"zusatzlotterien\"" },
                {CSSClassNames.AdditionalLottoGameEnd.ToString(), "class=\"accordion ui-accordion ui-widget ui-helper-reset\"" },
                {CSSClassNames.WinningQuotesLottoStart.ToString(), "responsive-table\"" },
                {CSSClassNames.WinningQuotesLottoEnd.ToString(), "class=\"info-box alert-box jackpot-box\"" }
            };


            return keyValuePairs;
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
        #endregion
    }
}
