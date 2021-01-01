using Android.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;

namespace App.Business
{
    public class TicketAnalyzer 
    {
        #region Fields
        private int _countHits;
        #endregion

        #region Propertys
        /// <summary>
        /// Die Treffer des Users
        /// </summary>
        public int CountHits
        {
            get
            {
                return _countHits;
            }
            set
            {
                _countHits = value;
            }
        }
        #endregion
        

        public TicketAnalyzer()
        {

        }



        private static string GetMoneyQuoteFromSpecialHit(int hits, bool superNumber, List<Quote> moneyQoutes)
        {
            string moneyAsFormatedString = "0,00€";
            //Der Modus wird nur alle 2 durchläufe erhöht, damit die Superzahlen prüfung abgeschlossen werden kann
            int mode = 2;
            int counter = 2;
            bool superNumberCheck = true;

            if (hits == 1)
                return moneyAsFormatedString;

            foreach (var qoute in moneyQoutes)
            {
                if (mode == hits && superNumber == superNumberCheck)
                {
                    moneyAsFormatedString = qoute.Money;
                    break;
                }

                if (counter % 2 == 0)
                {
                    mode++;
                }
                counter++;
                superNumberCheck = !superNumberCheck;
            }

            return moneyAsFormatedString;
        }

        /// <summary>
        /// Prüft die gezogene Superzahl mit der ausgewählten Superzahl
        /// </summary>
        /// <param name="userSuperNumber"></param>
        /// <param name="lottoSuperNumber"></param>
        /// <returns></returns>
        public static bool CheckIfSupernumberIsAnHit(int userSuperNumber, int lottoSuperNumber)
        {
            return userSuperNumber == lottoSuperNumber;
        }


        /// <summary>
        /// Konvertiert einen String in ein double
        /// </summary>
        /// <param name="inputString"></param>
        /// <returns></returns>
        public static double ToDouble(string inputString)
        {
            try
            {
                //NumberStyles styles = NumberStyles.AllowThousands | NumberStyles.AllowTrailingSign | NumberStyles.Number |
                //NumberStyles.AllowDecimalPoint;

                var cultureInfo = CultureInfo.GetCultureInfo("fr-FR");

                //InputString = String.Format(cultureInfo, "{0:C}", InputString);
            
                inputString = inputString.Remove(inputString.IndexOf('€')).Replace(" ","");

                if (inputString.Contains(','))
                {
                    string cent = inputString.Substring(inputString.IndexOf(',')).Replace(",","");
                    double centAsDouble = double.Parse($"0,{cent}", cultureInfo);
                    inputString = inputString.Remove(inputString.IndexOf(','));
                    double doubleMoney = double.Parse(inputString, NumberStyles.Currency);
                    return (doubleMoney + centAsDouble);
                }
                else
                {
                    double doubleMoney = double.Parse(inputString, NumberStyles.Currency);
                    return doubleMoney;
                }
            }
            catch (Exception ex)
            {
                Log.Warn("LottoscheinAuswerter", $"Fehler bei der Konvertierung in double {ex}");
                return 0;
            }     
        }

        /// <summary>
        /// Erstellt eine Gewinn-Auswertung übersicht
        /// </summary>
        /// <param name="listWinningInformations"></param>
        public static ObservableCollection<SparkleAnalysis> CreateWinningAnalysis(List<SparkleAnalysis> listWinningInformations, bool superNumber, List<Quote> moneyQoute)
        {
            ObservableCollection<SparkleAnalysis> tmpWinningAnalysis = new ObservableCollection<SparkleAnalysis>();

            //Für die auswertung ist es wichtig, dass die niedrigsten Gewinne oben stehen
            moneyQoute.Reverse();
            foreach (var item in listWinningInformations)
            {
                tmpWinningAnalysis.Add(new SparkleAnalysis(item.Lottozahlen, item.Hits, GetMoneyQuoteFromSpecialHit(item.Hits, superNumber, moneyQoute)));
            }
            return tmpWinningAnalysis;
        }
    }
}
