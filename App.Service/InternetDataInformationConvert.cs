using Android.Util;
using App.Business.LotteryTicket;
using App.Business.Web;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace App.Services
{
    public class WebsideDataConverter
    {
        #region Fields
        private CSSClasses _cSSClasses;
        private string _superSechsNumbersAsString;
        private string _spielSiebenundsiebzigAsString;
        private string _htmlSourceCode;
        private string _lottoWebsideURL = "http://www.lotto24.de/webshop/product/lottonormal/result";
        #endregion

        #region Propertys

        public Webside Webside { get; set; }

        public string HtmlSourceCode
        {
            get
            {
                return _htmlSourceCode;
            }

            set
            {
                _htmlSourceCode = value;
            }
        }

        public string[] WinningQuotesLotto
        {
            get
            {
                return GetQuotes(HtmlSourceCode, Webside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.WinningQuotesLottoStart.ToString()),
                    Webside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.WinningQuotesLottoEnd.ToString()), 1);
            }
        }

        /// <summary>
        /// Gibt die Gewinnqouten für Spiel 77 zurück
        /// </summary>
        public string[] WinningQuotesSpielSiebenundsiebzig => 
            GetQuotes(HtmlSourceCode,
                    Webside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.WinningQuotesSpielSiebenundsiebzigStart.ToString()),
                     Webside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.WinningQuotesSpielSiebenundsiebzigEnd.ToString()), 2);

        public ObservableCollection<LottoNumber> WinningNumbers
        {
            get
            {
                return ConvertObservableIntCollectionToLottonumberCollection(GetWinningNumbers(HtmlSourceCode, ClassNameBeginn, ClassNameEnds));
            }
        }

        private string ClassNameBeginn { get; set; }

        private string ClassNameEnds { get; set; }

        /// <summary>
        /// Gibt die gezogene Superzahl zurück
        /// </summary>
        public int SuperNumber => 
            GetSuperNumber(HtmlSourceCode, Webside.CSSClassDictionary.GetValueOrDefault(CSSClasses.CSSClassNames.SuperNumber.ToString()));

        public ObservableCollection<LottoNumber> SuperSechsNumbers
        {
            get
            {
                ObservableCollection<LottoNumber> tempNumbers = new ObservableCollection<LottoNumber>();

                foreach (char item in _superSechsNumbersAsString)
                {
                    tempNumbers.Add(new LottoNumber(item.ToString()));
                }
                return tempNumbers;
            }
        }

        public ObservableCollection<LottoNumber> SpielSiebenundsiebzigNumbers
        {
            get
            {
                ObservableCollection<LottoNumber> tempNumbers = new ObservableCollection<LottoNumber>();

                foreach (char item in _spielSiebenundsiebzigAsString)
                {
                    tempNumbers.Add(new LottoNumber(item.ToString()));
                }
                return tempNumbers;
            }
        }

        public ObservableCollection<LottoNumber> ConvertObservableIntCollectionToLottonumberCollection(ObservableCollection<int> tmpNumberCollection)
        {
            ObservableCollection<LottoNumber> tmpLottoCollection = new ObservableCollection<LottoNumber>();

            foreach (int item in tmpNumberCollection)
            {
                tmpLottoCollection.Add(new LottoNumber(item));
            }
            return tmpLottoCollection;
        }
        #endregion

        #region Constructor
        public WebsideDataConverter()
        {
            Webside = new Webside(_lottoWebsideURL);
            HtmlSourceCode = Webside.HTMLCode;

            if (!string.IsNullOrEmpty(HtmlSourceCode))
            {
                _cSSClasses = new CSSClasses();
                Log.Info("LottoscheinAuswerter", "Internetverbindung erfolgreich");
                ClassNameBeginn = "class=\"row winning-numbers\"";
                ClassNameEnds = "class=\"col-xs-12 col-sm-6 align-right hidden-xs\"";
                _spielSiebenundsiebzigAsString = GetSpiel77Numbers(HtmlSourceCode, "class=\"product-logo--tiny", "class=\"inner-table-header align-middle visible-xs-block");
                _superSechsNumbersAsString = GetSuperSechsNumbers(HtmlSourceCode, "class=\"product-logo--tiny", "class=\"inner-table-header align-middle visible-xs-block");
            }
            else
            {
                Log.Error("LottoscheinAuswerter", "Es war nicht möglich eine Verbindung zum Internet herzustellen");
            }
        }
        #endregion


        /// <summary>
        /// Gibt die Superzahl zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        private int GetSuperNumber(string htmlSource, string className)
        {
            string sourceCode = htmlSource;
            string resultString = "";
            int startIndex = htmlSource.IndexOf(className);

            try
            {
                if (startIndex < sourceCode.Length)
                {
                    string specialPart = sourceCode.Substring(startIndex);
                    resultString = Regex.Match(specialPart, @"\d+").Value;
                }

            }
            catch (Exception)
            {
                ShowInformationMassageAsync("Fehler", "Problem beim auslesen der Superzahl !").Start();
            }


            return Int32.Parse(resultString);
        }

        /// <summary>
        /// Gibt die gezogenen Zahlen zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <returns></returns>
        public ObservableCollection<int> GetWinningNumbers(string htmlSource, string className, string classNameEnd)
        {
            ObservableCollection<int> temp = new ObservableCollection<int>();
            int startIndex = htmlSource.IndexOf(className);
            int endindex = htmlSource.IndexOf(classNameEnd) - startIndex + 1;
            string specialPart = htmlSource.Substring(startIndex, endindex);
            string[] numbers = Regex.Split(specialPart, @"\D+");

            int counter = 0;

            foreach (string value in numbers)
            {
                if (!string.IsNullOrEmpty(value))
                {
                    int i = int.Parse(value);
                    counter++;

                    if (counter >= 3 && counter < 9)
                    {
                        temp.Add(i);
                    }
                    if (counter == 9)
                        break;
                }
            }

            return temp;
        }

        #region GetQuotes

        /// <summary>
        /// Gibt die Gewinnquoten zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <param name="mode"> 1 => Lotto, 2 => Spiel 77/Super 6</param>
        /// <returns></returns>
        private string[] GetQuotes(string htmlSource, string className, string classNameEnd, int mode)
        {
            int startIndex = 0;
            int endindex = 0;

            if (mode == 1)
            {
                startIndex = htmlSource.IndexOf(className);
                endindex = htmlSource.IndexOf(classNameEnd) - startIndex + 1;
            }
            else
            {
                startIndex = IndexOfSecond(htmlSource, className);
                endindex = htmlSource.IndexOf(classNameEnd) - startIndex + 1;
                endindex = endindex * -1;
            }

            string[] QuotesAsStringArray;

            if (startIndex <= 0 || endindex <= 0)
            {
                QuotesAsStringArray = new string[0];
            }
            else
            {
                string specialPart = htmlSource.Substring(startIndex, endindex);

                if (!specialPart.Contains("wird ermittelt"))
                {
                    if (mode == 1)
                    {
                        QuotesAsStringArray = GetMoneyQoutesFromSubstring(specialPart);
                    }
                    else
                    {
                        var arr = Regex.Matches(specialPart, @"(\d{1,9})(.\d{1,9}|)(.\d{1,9})")
                                  .Cast<Match>()
                                  .Select(m => m.Value)
                                  .ToArray();
                        QuotesAsStringArray = arr;
                    }
                }
                else
                {
                    QuotesAsStringArray = new string[0];
                }
            }

            return QuotesAsStringArray;
        }

        private string[] GetMoneyQoutesFromSubstring(string specialPart)
        {
            string result = "";
            string currentSecialPart = specialPart;

            try
            {
                //Gibt die Tabellenzeile an, wo sich die Gewinne befinden
                int tableRowCounter = 0;

                //Geht jede mögliche Gewinnklasse (also 9 mal) durch
                for (int i = 0; i < 9; i++)
                {
                    int pFrom = currentSecialPart.IndexOf("<span class=\"visible-xs\">") + "<span class=\"visible-xs\">".Length;
                    var a = currentSecialPart.Substring(pFrom + "<span class=\"visible-xs\">".Length);
                    int pTo;
                    
                    //Prüft, ob dies der letzte Durchgang ist
                    if (i != 8)
                    {
                        //Prüft, ob dies der erste durchlauf ist
                        tableRowCounter = (i == 0) ? tableRowCounter += 2 : tableRowCounter += 1;

                        pTo = a.IndexOf($"<div class=\"inner-table-row\" data-test-id=\"tableRow{tableRowCounter}\">");
                    }
                    else
                    {
                        pTo = a.IndexOf("<div class=\"product-table__content\">");
                    }

                    result += RemoveWhitespace(currentSecialPart.Substring(pFrom, pTo));
                    currentSecialPart = a.Substring(pTo);
                }
                return Regex.Matches(result, @"(\d{1,9})(.\d{1,9}|)(.\d{1,9})")
                            .Cast<Match>()
                            .Select(m => m.Value)
                            .ToArray();
            }
            catch (Exception ex)
            {
                Log.Error("LottoscheinAuswerter", ex.ToString());
                return new string[0];
            }
        }

        #endregion

        /// <summary>
        /// Prüft, ob ein Substring ein Escape-Symbol (\n) enthält
        /// </summary>
        /// <param name="specialpart"></param>
        /// <returns></returns>
        private bool CheckIfSubstringContainsEscapeSymbol(string specialpart)
        {
            return specialpart.Contains("\n") ? false : true;
        }

        /// <summary>
        /// Entfernt die Leerzeichen in einem string
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        private string RemoveWhitespace(string input)
        {
            if (CheckIfSubstringContainsEscapeSymbol(input))
            {
                input = input.Replace("\n", " ");
            }


            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        /// <summary>
        /// Gibt die gezogenen Super 6 Zahlen zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <returns></returns>
        public string GetSuperSechsNumbers(String htmlSource, String className, String classNameEnd)
        {
            int startIndex = IndexOfSecond(htmlSource, className);

            int endindex = htmlSource.IndexOf(classNameEnd) - startIndex + 1;
            string[] temp;

            try
            {
                string specialPart = htmlSource.Substring(startIndex);

                if (!specialPart.Contains("wird ermittelt"))
                {

                    var arr = Regex.Matches(specialPart, @"\d+")
                                .Cast<Match>()
                                .Select(m => m.Value)
                                .ToArray();
                    temp = arr;
                }
                else
                {
                    temp = new string[0];
                }
            }
            catch (ArgumentException)
            {
                ShowInformationMassageAsync("Fehler", string.Format("Ein Fehler bein auslesen der Webseite ist aufgetren !"));

                temp = new string[0];
            }
            catch
            {
                ShowInformationMassageAsync("Fehler", "Ein unerwarteter Fehler ist bei Auslesen der Superzahl aufgetreten !");

                temp = new string[0];
            }
            string numbers = temp[5];
            return numbers;
        }

        private int IndexOfSecond(string theString, string toFind)
        {
            int first = theString.IndexOf(toFind);

            if (first == -1) return -1;

            // Find the "next" occurrence by starting just past the first
            return theString.IndexOf(toFind, first + 1);
        }

        /// <summary>
        /// Gibt die Gewinnzahlen aus Spiel77 zurück
        /// </summary>
        /// <param name="htmlSource"></param>
        /// <param name="className"></param>
        /// <param name="classNameEnd"></param>
        /// <returns></returns>
        public string GetSpiel77Numbers(string htmlSource, string className, string classNameEnd)
        {
            int startIndex = htmlSource.IndexOf(className);
            int endindex = htmlSource.IndexOf(classNameEnd) - startIndex + 1;
            string[] temp;

            try
            {
                string specialPart = htmlSource.Substring(startIndex, endindex);

                if (!specialPart.Contains("wird ermittelt"))
                {
                    var arr = Regex.Matches(specialPart, @"\d+")
                                .Cast<Match>()
                                .Select(m => m.Value)
                                .ToArray();

                    temp = arr;
                }
                else
                {
                    temp = new string[0];
                }
            }
            catch (ArgumentException ae)
            {
                ShowInformationMassageAsync("Fehler", string.Format("Ein Fehler bein auslesen der Webseite ist aufgetren !", ae)).Start();
                temp = new string[0];
            }
            catch
            {
                ShowInformationMassageAsync("WARNUNG", "Ein unerwarteter Fehler ist bei Auslesen des Spiel77 aufgetreten !").Start();
                temp = new string[0];
            }
            string numbers = temp[5];
            return numbers;
        }

        private async Task ShowInformationMassageAsync(string titel, string text)
        {
            //await  MainPage.DisplayAlert(titel, text, "OK");
        }

    }
}
