using App.Business;
using App.Business.Converter;
using App.Business.LotteryTicket;
using App.Business.LuckyGame;
using App.Business.Web;
using App.Services;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace App.Service.Web
{
    public class LottoWebsideProvider
    {
        #region Fields
        private const int DefaultSuperNumber = -1;
        private static NLog.Logger logger;
        private readonly string _lottoWebsideURL;
        private static string _websideMessageWhenDataIsNotReadyYet = "wird ermittelt";
        private LottolandApiResult _lottolandResult;
        #endregion

        #region Propertys
        public Webside LottoWebside { get; set; }

        public string HTML { get; set; }

        public List<Quote> WinningQuotesLotto =>  GetQuotes(Games.Varietie.Lotto, _lottolandResult);

        /// <summary>
        /// Gibt die Gewinnqouten für Spiel 77 zurück
        /// </summary>
        public List<Quote> WinningQuotesSpiel77 =>
            GetQuotes(Games.Varietie.Spiel77, _lottolandResult);

        /// <summary>
        /// Gibt die Gewinnqouten für Spiel 77 zurück
        /// </summary>
        public List<Quote> WinningQuotesSuper6 =>
            GetQuotes(Games.Varietie.Super6, _lottolandResult);

        public ObservableCollection<long> WinningNumbers
        {
            get
            {
                return (_lottolandResult?.Last?.Numbers != null) 
                    ? new ObservableCollection<long>(_lottolandResult.Last.Numbers) 
                    : new ObservableCollection<long>();
            }
        }

        /// <summary>
        /// Gibt die gezogene Superzahl zurück
        /// </summary>
        public int SuperNumber => _lottolandResult?.Last?.Superzahl ?? DefaultSuperNumber;
            
        /// <summary>
        /// Gibt die gezogenen SuperSechs Zahlen zurück
        /// </summary>
        public ObservableCollection<LottoNumber> SuperSechsNumbers
        {
            get
            {
                ObservableCollection<LottoNumber> tempNumbers = new ObservableCollection<LottoNumber>();
                try
                {
                    var addiotionalLottoNumbers = _lottolandResult?.Last?.Super6?.ToCharArray();

                    if (addiotionalLottoNumbers != null)
                    {
                        foreach (var item in addiotionalLottoNumbers)
                            tempNumbers.Add(new LottoNumber(item));
                    }
                }
                catch (Exception ex)
                {
                    logger.Warn($"Fehler beim auslesen der SuperSechsNumbers {ex}");
                }
               
 
                return tempNumbers;
            }
        }

        /// <summary>
        /// Gibt die gezogenen SpielSiebenundsiebzig Zahlen zurück
        /// </summary>
        public ObservableCollection<LottoNumber> SpielSiebenundsiebzigNumbers
        {
            get
            {
                ObservableCollection<LottoNumber> tempNumbers = new ObservableCollection<LottoNumber>();
                try
                {
                    var addiotionalLottoNumbers = _lottolandResult?.Last?.Spiel77?.ToCharArray();

                    if (addiotionalLottoNumbers != null)
                    {
                        foreach (var item in addiotionalLottoNumbers)
                            tempNumbers.Add(new LottoNumber(item));
                    }
                }
                catch (Exception ex)
                {
                    logger.Warn($"Fehler beim auslesen der SpielSiebenundsiebzigNumbers {ex}");
                }
                return tempNumbers;
            }
        }
        #endregion

        public LottoWebsideProvider(string url)
        {
            _lottoWebsideURL = url;
            DownloadAndPrepareLottoInformation();
        }

        #region Methdos

        /// <summary>
        /// Gibt die Gewinnquoten zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <returns></returns>
        private List<Quote> GetQuotes(Games.Varietie mode, LottolandApiResult lottolandApiResult)
        {
            List<Quote> quoteList = new List<Quote>();

            try
            {
                switch (mode)
                {
                    case Games.Varietie.Lotto:
                        foreach (var element in lottolandApiResult.Last.Odds.OrderBy(x => x.Key))
                        {
                            quoteList.Add(ConvertOddToQoute(element));
                        }
                        break;
                    case Games.Varietie.Super6:
                        foreach (var element in lottolandApiResult.Last.Super6Odds)
                        {
                            quoteList.Add(ConvertOddToQoute(element));
                        }
                        break;
                    case Games.Varietie.Spiel77:
                        foreach (var element in lottolandApiResult.Last.Spiel77Odds)
                        {
                            quoteList.Add(ConvertOddToQoute(element));
                        }
                        break;
                    default:
                        break;
                }

                //Im ersten Eintrag befinden sich immer 0€
                quoteList.RemoveAt(0);
            }
            catch (Exception ex)
            {
                logger.Warn($"Leider können keine Quoten des {mode} ausgelesen werden: {ex}");
            }


            return quoteList;
        }

        private Quote ConvertOddToQoute(KeyValuePair<string, Odd> keyOddPair)
        {
            var moneyInCents = MoneyConverter.CurrencyConversion(keyOddPair.Value.Prize, "AUD", "EUR");
            var moneyInEuro = MoneyConverter.CentToEuros(moneyInCents);
            return new Quote(keyOddPair.Key, keyOddPair.Value.Winners, MoneyConverter.DoubleToEuros(moneyInEuro));
        }

        /// <summary>
        /// Entfert unnötige EInträge aus der HTML Datei
        /// </summary>
        private void CleanHTML()
        {
            if (HTML.Contains("</head>"))
                HTML = HTML.Substring(HTML.IndexOf("</head>"));
            if(HTML.Contains("HeaderBasketLink"))
                HTML = HTML.Substring(HTML.IndexOf("HeaderBasketLink"));

        }

        private LottolandApiResult LoadLotteryInformationsFromLottolandApi(string json)
        {
            LottolandApiResult lottolandApiResult = new LottolandApiResult(); 
            try
            {
                //string jsonWithoutHint = json.Remove(json.IndexOf('"'), (json.IndexOf('"', json.IndexOf('"')+1)+1) );
                //var b = JsonConvert.DeserializeObject<List<LotteryWinningNumbers>>(jsonWithoutHint, new LotteryWinningNumberElementConverter());
                lottolandApiResult = JsonConvert.DeserializeObject<LottolandApiResult>(json);
               
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Fehler beim laden der Lottozahlen");
            }
            return lottolandApiResult;
        }

        private async void DownloadAndPrepareLottoInformation()
        {
            LottoWebside = new Webside(_lottoWebsideURL);
            
            if (Xamarin.Essentials.Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.None)
            {
                HTML = LottoWebside.HTMLCode;
                _lottolandResult = LoadLotteryInformationsFromLottolandApi(HTML);
            }
            else
            {
                _lottolandResult = new LottolandApiResult();
                await ShowInformationMassageAsync("Info", "Bitte schalten Sie ihr Internet ein und starten Sie die App neu ;)");
            }

         
            logger = NLog.LogManager.GetLogger("logfile");
        }

        private async Task ShowInformationMassageAsync(string titel, string text)
        {
            await App.UI.App.Current.MainPage.DisplayAlert(titel, text, "OK");
        }
        #endregion
    }
}
