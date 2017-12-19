using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace TestApp.Services
{
    public class WebsideDataConverter
    {
        #region Fields
        private InternetConnection _connection;
        private ObservableCollection<int> _winningNumbers;
        private string _superSechsNumbersAsString;
        private string _spielSiebenundsiebzigAsString;
        private string _htmlSourceCode;
        private string _lottoWebsideURL = "http://www.lotto24.de/webshop/product/lottonormal/result";

        #endregion
        
        public WebsideDataConverter()
        {
            WebsideContent = new InternetConnection(_lottoWebsideURL);
            HtmlSourceCode = WebsideContent.HtmlQuellcode;

            if (!string.IsNullOrEmpty(HtmlSourceCode))
            {
                ClassNameBeginn = "class=\"row winning-numbers\"";
                ClassNameEnds = "class=\"col-xs-12 col-sm-6 align-right hidden-xs\"";
                _spielSiebenundsiebzigAsString = GetSpiel77Numbers(HtmlSourceCode, "class=\"product-logo--tiny", "class=\"inner-table-header align-middle visible-xs-block");
                _superSechsNumbersAsString = GetSuperSechsNumbers(HtmlSourceCode, "class=\"product-logo--tiny", "class=\"inner-table-header align-middle visible-xs-block");
            }
            else
            {
                ShowInformationMassageAsync("Verbindung fehlgeschlagen", "Es war nicht möglich eine Verbindung zum Internet herzustellen");
            }
        }

        public int GetSuperNumber(string htmlSource, string className)
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
                ShowInformationMassageAsync("Fehler","Problem beim auslesen der Superzahl !");
            }


            return Int32.Parse(resultString);
        }

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
        /// <param name="mode"></param>
        /// <returns></returns>
        public string[] GetQuotes(string htmlSource, string className, string classNameEnd, int mode)
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

            string specialPart = htmlSource.Substring(startIndex, endindex);
            string[] QuotesAsStringArray;

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

            return QuotesAsStringArray;
        }

        private string[] GetMoneyQoutesFromSubstring(string specialPart)
        {
            string result = "";
            string currentSecialPart = specialPart;

            for (int i = 0; i < 9; i++)
            {
                int pFrom = currentSecialPart.IndexOf("<span class=\"visible-xs\">") + "<span class=\"visible-xs\">".Length;
                var a = currentSecialPart.Substring(pFrom + "<span class=\"visible-xs\">".Length);
                int pTo;

                if (i != 8)
                {
                    pTo = a.IndexOf("<div class=\"col-xs-2 col-sm-2\">");

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

        #endregion


        private bool CheckIfSubstringContainsEscapeSymbol(string specialpart)
        {
            return specialpart.Contains("\n") ? false : true;
        }

        public string RemoveWhitespace(string input)
        {
            if (CheckIfSubstringContainsEscapeSymbol(input))
            {
                input = input.Replace("\n", " ");
            }


            return new string(input.ToCharArray()
                .Where(c => !Char.IsWhiteSpace(c))
                .ToArray());
        }

        public ObservableCollection<Business.LottoNumber> ConvertObservableIntCollectionToLottonumberCollection(ObservableCollection<int> tmpNumberCollection)
        {
            ObservableCollection<Business.LottoNumber> tmpLottoCollection = new ObservableCollection<Business.LottoNumber>();

            foreach (int item in tmpNumberCollection)
            {
                tmpLottoCollection.Add(new Business.LottoNumber(item));
            }
            return tmpLottoCollection;
        }


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
            string numbers = temp[4];
            return numbers;
        }

        private int IndexOfSecond(string theString, string toFind)
        {
            int first = theString.IndexOf(toFind);

            if (first == -1) return -1;

            // Find the "next" occurrence by starting just past the first
            return theString.IndexOf(toFind, first + 1);
        }


        public string GetSpiel77Numbers(String htmlSource, String className, String classNameEnd)
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
                ShowInformationMassageAsync("Fehler", string.Format("Ein Fehler bein auslesen der Webseite ist aufgetren !", ae));
                temp = new string[0];
            }
            catch
            {
                ShowInformationMassageAsync("Ein unerwarteter Fehler ist bei Auslesen des Spiel77 aufgetreten !", "WARNUNG");
                temp = new string[0];
            }
            string numbers = temp[4];
            return numbers;
        }

        private async System.Threading.Tasks.Task ShowInformationMassageAsync(string titel, string text)
        {
            await App.Current.MainPage.DisplayAlert(titel, text, "OK");
        }

        #region Propertys

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
                return GetQuotes(HtmlSourceCode, "class=\"inner-table-header align-middle hidden-xs", "class=\"inner-table-header align-middle visible-xs-block", 1);
            }
        }

        public string[] WinningQuotesSpielSiebenundsiebzig
        {
            get
            {
                return GetQuotes(HtmlSourceCode, "class=\"inner-table-header align-middle hidden-xs", "class=\"inner-table-header align-middle hidden-xs", 2);
            }
        }

        public ObservableCollection<Business.LottoNumber> WinningNumbers
        {
            get
            {
                return ConvertObservableIntCollectionToLottonumberCollection(GetWinningNumbers(HtmlSourceCode, ClassNameBeginn, ClassNameEnds));
            }
        }

        internal InternetConnection WebsideContent
        {
            get
            {
                return _connection;
            }

            set
            {
                _connection = value;
            }
        }

        private string ClassNameBeginn { get; set; }

        private string ClassNameEnds { get; set; }

        public int SuperNumber
        {
            get
            {
                return GetSuperNumber(HtmlSourceCode, "class=\"winning-numbers__number winning-numbers__number--superzahl\"");
            }
        }

        public ObservableCollection<string> SuperSechsNumbers
        {
            get
            {
                ObservableCollection<string> tempNumbers = new ObservableCollection<string>();

                foreach (char item in _superSechsNumbersAsString)
                {
                    tempNumbers.Add(item.ToString());
                }
                return tempNumbers;
            }
        }

        public ObservableCollection<string> SpielSiebenundsiebzigNumbers
        {
            get
            {
                ObservableCollection<string> tempNumbers = new ObservableCollection<string>();

                foreach (char item in _spielSiebenundsiebzigAsString)
                {
                    tempNumbers.Add(item.ToString());
                }
                return tempNumbers;
            }
        }
    }
    #endregion
}
