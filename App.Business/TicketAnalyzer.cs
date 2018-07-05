using Android.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;
using TestApp.Services;

namespace TestApp.Business
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



        private static string GetMoneyQuoteFromSpecialHit(int hits, bool superNumber, string[] moneyQoutes)
        {
            string moneyForUser = "0,00";
            double money = 0;
            int mode = 2;
            bool check = true;

            for (int i = 2; i <= 10; i++)
            {
                if (mode == hits && superNumber == check)
                {
                    moneyForUser = CompareQuoteToHits(i - 1, moneyQoutes);
                    money += ToDouble(moneyForUser);
                    break;
                }
                if (i % 2 == 0)
                {
                    mode++;
                }
                if (check)
                {
                    check = false;
                }
                else
                {
                    check = true;
                }
            }

            return ToMoney(money);
        }

        /// <summary>
        /// Bekommt den Gewinn zurueckgegeben, Uebergabeparamter ist das Qoutenlevel
        /// </summary>
        /// <param name="pQuoteLevel"></param>
        /// <returns></returns>
        private static string CompareQuoteToHits(int pQuoteLevel, string[] moneyQoutes)
        {
            string money = "0,00";

            switch (pQuoteLevel)
            {
                case 1:
                    money = moneyQoutes[14]; //20
                    break;
                case 2:
                    money = moneyQoutes[12]; //17
                    break;
                case 3:
                    money = moneyQoutes[10]; //14
                    break;
                case 4:
                    money = moneyQoutes[8]; //11
                    break;
                case 5:
                    money = moneyQoutes[6]; //8
                    break;
                case 6:
                    money = moneyQoutes[4]; //5
                    break;
                case 7:
                    money = moneyQoutes[2]; //2
                    break;
                case 8:
                    money = moneyQoutes[1]; //1
                    break;
                case 9:
                    money = moneyQoutes[0]; //0
                    break;
                default:
                    money = "0,00";
                    break;
            }
            Log.Info("LottoscheinAuswerter", string.Format("Der Gewinn fuer Lotto betraegt: {0} €", money));
            return string.Format("{0} €", money);
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
        /// Wandelt einen double Wert in Euro um
        /// </summary>
        /// <param name="In">Wert</param>
        /// <returns>Betrag</returns>
        private static string ToMoney(double In)
        {
            return In.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"));
        }


        /// <summary>
        /// Convertiert einen String in ein double
        /// </summary>
        /// <param name="In"></param>
        /// <returns></returns>
        public static double ToDouble(string In)
        {
            NumberStyles styles = NumberStyles.AllowThousands | NumberStyles.AllowTrailingSign | NumberStyles.Number |
            NumberStyles.AllowDecimalPoint;
            In = In.Remove(In.IndexOf('€'));
            double Double = double.Parse(In, styles);
            return Double;
        }

        /// <summary>
        /// Erstellt eine Gewinn-Auswertung übersicht
        /// </summary>
        /// <param name="listWinningInformations"></param>
        public static ObservableCollection<SparkleAnalysis> CreateWinningAnalysis(List<SparkleAnalysis> listWinningInformations, bool superNumber, string[] moneyQoute)
        {
            ObservableCollection<SparkleAnalysis> tmpWinningAnalysis = new ObservableCollection<SparkleAnalysis>();
            foreach (var item in listWinningInformations)
            {
                tmpWinningAnalysis.Add(new SparkleAnalysis(item.Lottozahlen, item.Hits, GetMoneyQuoteFromSpecialHit(item.Hits, superNumber, moneyQoute)));
            }
            return tmpWinningAnalysis;
        }
    }
}
