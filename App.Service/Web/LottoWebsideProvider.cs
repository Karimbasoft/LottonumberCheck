﻿using App.Business;
using App.Business.LotteryTicket;
using App.Business.LuckyGame;
using App.Business.Web;
using App.Services;
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
        private static NLog.Logger logger;
        private readonly string _lottoWebsideURL;
        private WebsideDataConverter lottoService;
        private static string _websideMessageWhenDataIsNotReadyYet = "wird ermittelt";
        #endregion

        #region Propertys
        public Webside LottoWebside { get; set; }

        public string HTML { get; set; }

        public List<Quote> WinningQuotesLotto
        {
            get =>  GetQuotes(Games.Varietie.Lotto);
        }

        /// <summary>
        /// Gibt die Gewinnqouten für Spiel 77 zurück
        /// </summary>
        public List<Quote> WinningQuotesSpiel77 =>
            GetQuotes(Games.Varietie.Spiel77);

        /// <summary>
        /// Gibt die Gewinnqouten für Spiel 77 zurück
        /// </summary>
        public List<Quote> WinningQuotesSuper6 =>
            GetQuotes(Games.Varietie.Super6);

        public ObservableCollection<LottoNumber> WinningNumbers
        {
            get
            {
                var winningNumbers = GetWinningNumberData();
                winningNumbers.RemoveAt(winningNumbers.Count() - 1);
                return ConvertIntCollectionToLottonumberCollection(winningNumbers);
            }
        }

        


        ///// <summary>
        ///// Gibt die gezogene Superzahl zurück
        ///// </summary>
        public int SuperNumber => (WinningNumbers != null && WinningNumbers.Count > 6) ? WinningNumbers[6].Number : 0;

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
                    
                    var addiotionalLottoNumbers = GetAdditionalLottoGameNumbers();

                    if (addiotionalLottoNumbers != null)
                    {
                        if (addiotionalLottoNumbers.Count() == 13)
                        {
                            for (int i = 8; i < 14; i++)
                                tempNumbers.Add(new LottoNumber(addiotionalLottoNumbers[i]));
                        }
                        else
                        {
                            foreach (char item in addiotionalLottoNumbers[1])
                                tempNumbers.Add(new LottoNumber(item.ToString()));
                        }
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
                    var addiotionalLottoNumbers = GetAdditionalLottoGameNumbers();

                    if (addiotionalLottoNumbers != null)
                    {
                        if (addiotionalLottoNumbers.Count() == 13)
                        {
                            for (int i = 0; i <= 6; i++)
                                tempNumbers.Add(new LottoNumber(addiotionalLottoNumbers[i]));
                        }
                        else
                        {
                            foreach (char item in addiotionalLottoNumbers[0])
                                tempNumbers.Add(new LottoNumber(item.ToString()));
                        }
                        
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
            LottoWebside = new Webside(_lottoWebsideURL);
            HTML = LottoWebside.HTMLCode;
            logger = NLog.LogManager.GetLogger("logfile");
            CleanHTML();
        }

        #region Methdos
        /// <summary>
        /// Konvertiert eine int Collection in eine LottonumberCOllection
        /// </summary>
        /// <param name="tmpNumberCollection"></param>
        /// <returns></returns>
        public ObservableCollection<LottoNumber> ConvertIntCollectionToLottonumberCollection(ObservableCollection<int> tmpNumberCollection)
        {
            ObservableCollection<LottoNumber> tmpLottoCollection = new ObservableCollection<LottoNumber>();

            foreach (int item in tmpNumberCollection)
            {
                tmpLottoCollection.Add(new LottoNumber(item));
            }
            return tmpLottoCollection;
        }

        /// <summary>
        /// Gibt die gezogenen ZAhlen in einer synchronen Methode zurück
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<int> GetWinningNumberData()
        {
            var task = Task.Run(async () => await GetWinningNumbersAsync());
            return (task.Status == TaskStatus.Faulted || task.Status == TaskStatus.Canceled) ? null : task.Result;
        }

        private string[] GetAdditionalLottoGameNumbers()
        {
            return Task.Run(async () => await GetAdditionalLottoGameNumbersAsync()).Result;
        }

        /// <summary>
        /// Gibt die gezogenen Zahlen zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <returns></returns>
        private async Task<ObservableCollection<int>> GetWinningNumbersAsync()
        {
             return await Task.Run(async () =>
             {
                 ObservableCollection<int> temp = new ObservableCollection<int>();

                 try
                 {
                     var test = LottoWebside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.LottoNumberStart.ToString());
                     int startIndex = HTML.IndexOf(LottoWebside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.LottoNumberStart.ToString()));
                     int endindex = HTML.IndexOf(LottoWebside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.LottoNumberEnd.ToString())) - startIndex + 1;
                     string specialPart = HTML.Substring(startIndex, endindex);
                     string[] numbers = Regex.Split(specialPart, @"\D+");
                     numbers = RemoveWhiteSpaceFromStringArray(numbers);

                     if (numbers.Any())
                        numbers = numbers.Count() > 7 ? numbers.Take(numbers.Count() -1).ToArray() : numbers.ToArray();

                     //Füllt die temp Liste
                     numbers.ToList().ForEach(num => temp.Add(int.Parse(num)));
                 }
                 catch (Exception ex)
                 {
                     await ShowInformationMassageAsync("Fehler", string.Format($"Ein Fehler bein auslesen der Lottozahlen ist aufgetren: {ex}"));
                     logger.Error($"Fehler beim auslesen der Gewinnzahlen: {ex}");
                 }

                 return temp;
             });
        }

        /// <summary>
        /// Gibt die gezogenen Super 6 Zahlen zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <returns></returns>
        private async Task<string[]> GetAdditionalLottoGameNumbersAsync()
        {
            return await Task.Run(async () =>
            {
                int startIndex = HTML.IndexOf(LottoWebside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.AdditionalLottoGameStart.ToString()));
                int endindex = HTML.IndexOf(LottoWebside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.AdditionalLottoGameEnd.ToString())) - startIndex + 1;
                string[] temp;
                try
                {
                    string specialPart = HTML.Substring(startIndex, endindex);

                    if (!specialPart.Contains("wird ermittelt"))
                    {

                        var arr = Regex.Matches(specialPart, @"\d+")
                                    .Cast<Match>()
                                    .Select(m => m.Value)
                                    .ToArray();

                        if (arr.Count() == 13)
                        {
                            //Nur die ersten 14 Zahlen werden gebraucht
                            temp = arr.SubArray(0, 14);
                        }
                        else
                        {
                            temp = new string[] { arr[1], arr[3] };
                        }
                        
                    }
                    else
                    {
                        temp = new string[0];
                    }
                }
                catch (Exception ex)
                {
                    await ShowInformationMassageAsync("Fehler", string.Format($"Ein Fehler bein auslesen der Spiel77/Super ist aufgetren: {ex}"));
                    temp = new string[0];
                }
            return temp;
            });
        }

        /// <summary>
        /// Gibt die Gewinnquoten zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <returns></returns>
        private List<Quote> GetQuotes(Games.Varietie mode)
        {
            int startIndex = 0;
            int endindex = 0;
            List<Quote> quoteList = new List<Quote>();

            try
            {
                switch (mode)
                {
                    case Games.Varietie.Lotto:
                        startIndex = HTML.IndexOf(LottoWebside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.WinningQuotesLottoStart.ToString()));
                        endindex = HTML.IndexOf(LottoWebside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.WinningQuotesLottoEnd.ToString())) - startIndex + 1;
                        break;
                    case Games.Varietie.Super6:
                    case Games.Varietie.Spiel77:
                        //startIndex = IndexOfSecond(htmlSource, className);
                        //endindex = htmlSource.IndexOf(classNameEnd) - startIndex + 1;
                        break;
                    default:
                        break;
                }


                if (startIndex <= 0 || endindex <= 0)
                {
                    throw new Exception("Index zum auslesen stimmt nicht mit HTML überein");
                }
                else
                {
                    string specialPart = HTML.Substring(startIndex, endindex);

                    if (mode == Games.Varietie.Lotto)
                    {
                        quoteList = GetMoneyQoutesFromSubstring(specialPart);
                    }
                    else if (mode == Games.Varietie.Super6 || mode == Games.Varietie.Spiel77)
                    {
                        //var arr = Regex.Matches(specialPart, @"(\d{1,9})(.\d{1,9}|)(.\d{1,9})")
                        //          .Cast<Match>()
                        //          .Select(m => m.Value)
                        //          .ToArray();
                        //quotesAsStringArray = arr;
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Warn($"Leider können keine Quoten des {mode.ToString()} ausgelesen werden: {ex}");
            }


            return quoteList;
        }

        private List<Quote> GetMoneyQoutesFromSubstring(string specialPart)
        {
            string[] separatingCharsForEachQutes = { "<tr>", "</tr>" };
            string[] separatingCharsForEachAtribute = { "<td>", "</td>" };
            List<Quote> qouteInfos = new List<Quote>();
            int winningClasses = 9;
            string currentSecialPart = specialPart;

            try
            {
                //Entfernt den head Berecih drr Tabelle
                currentSecialPart = currentSecialPart.Remove(0, currentSecialPart.IndexOf("<tbody>"));
                
                //Fast die Inforamtionen in ein Array zusammen, anhand der Tabellenspalte
                var trData = currentSecialPart.Split(separatingCharsForEachQutes, System.StringSplitOptions.RemoveEmptyEntries);

                //Der erste Eintrag ist nicht brauchbar
                trData = trData.Skip(1).ToArray();
                
                //Geht jede mögliche Gewinnklasse (also 9 mal) durch
                for (int i = 0; i < winningClasses; i++)
                {       
                    var subInfos = trData[i].Split(separatingCharsForEachAtribute, System.StringSplitOptions.RemoveEmptyEntries);

                    //Wenn keine Geldbeträge da sind, wird auch kein Geld ausgegeben
                    if (subInfos[2].Equals(_websideMessageWhenDataIsNotReadyYet))
                    {
                        qouteInfos.Clear();
                        break;
                    }
                    
                    //Fügt die Einträge der Qoutenliste hinzu, wenn keine Gewinner Daten da sind, wird diese als 0 vermerkt
                    qouteInfos.Add(new Quote(subInfos[0], (!subInfos[1].Equals(_websideMessageWhenDataIsNotReadyYet)) ? int.Parse(subInfos[1]) : 0, subInfos[2]));
                    
                }
            }
            catch (Exception ex)
            {
                //Log.Error("LottoscheinAuswerter", ex.ToString());
                return new List<Quote>();
            }
            return qouteInfos;
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

        /// <summary>
        /// Enfernt leere Array-Einträge
        /// </summary>
        /// <param name="toConvertArry"></param>
        /// <returns></returns>
        private string[] RemoveWhiteSpaceFromStringArray(string[] toConvertArry)
        {
            foreach (var item in toConvertArry)
            {
                if (item.Equals(""))
                {
                    toConvertArry = toConvertArry.Where(val => val != item).ToArray();

                }
            }
            return toConvertArry;
        }

        private async Task ShowInformationMassageAsync(string titel, string text)
        {
            await App.UI.App.Current.MainPage.DisplayAlert(titel, text, "OK");
        }
        #endregion
    }
}
