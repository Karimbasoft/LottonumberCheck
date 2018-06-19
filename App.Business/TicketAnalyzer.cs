using Android.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text;

namespace TestApp.Business
{
    public class TicketAnalyzer 
    {
        private int _countHits;

        

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

        public TicketAnalyzer()
        {

        }


        private static string GetMoneyQuoteFromSpecialHit(int hits)
        {
            string moneyForUser = "0,00";
            double money = 0;
            int mode = 2;
            bool check = true;

            //for (int i = 2; i <= 10; i++)
            //{
            //    if (mode == hits && CheckIfSupernumberIsAnHit() == check)
            //    {
            //        moneyForUser = CompareQuoteToHits(i - 1);
            //        money += ToDouble(moneyForUser);
            //        break;
            //    }
            //    if (i % 2 == 0)
            //    {
            //        mode++;
            //    }
            //    if (check)
            //    {
            //        check = false;
            //    }
            //    else
            //    {
            //        check = true;
            //    }
            //}
            return ToMoney(money);
        }

        /// <summary>
        /// Bekommt den Gewinn zurueckgegeben, Uebergabeparamter ist das Qoutenlevel
        /// </summary>
        /// <param name="pQuoteLevel"></param>
        /// <returns></returns>
        private string CompareQuoteToHits(int pQuoteLevel)
        {
            string money = "0,00";

            //switch (pQuoteLevel)
            //{
            //    case 1:
            //        money = MoneyQuotes[14]; //20
            //        break;
            //    case 2:
            //        money = MoneyQuotes[12]; //17
            //        break;
            //    case 3:
            //        money = MoneyQuotes[10]; //14
            //        break;
            //    case 4:
            //        money = MoneyQuotes[8]; //11
            //        break;
            //    case 5:
            //        money = MoneyQuotes[6]; //8
            //        break;
            //    case 6:
            //        money = MoneyQuotes[4]; //5
            //        break;
            //    case 7:
            //        money = MoneyQuotes[2]; //2
            //        break;
            //    case 8:
            //        money = MoneyQuotes[1]; //1
            //        break;
            //    case 9:
            //        money = MoneyQuotes[0]; //0
            //        break;
            //    default:
            //        money = "0,00";
            //        break;
            //}
            //Log.Info("LottoscheinAuswerter", string.Format("Der Gewinn fuer Lotto betraegt: {0} €", money));
            return string.Format("{0} €", money);
        }

        private bool CheckIfSupernumberIsAnHit(int userSuperNumber, int lottoSuperNumber)
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
        public static ObservableCollection<SparkleAnalysis> CreateWinningAnalysis(List<SparkleAnalysis> listWinningInformations)
        {
            ObservableCollection<SparkleAnalysis> tmpWinningAnalysis = new ObservableCollection<SparkleAnalysis>();
            foreach (var item in listWinningInformations)
            {
                tmpWinningAnalysis.Add(new SparkleAnalysis(item.Lottozahlen, item.Hits, GetMoneyQuoteFromSpecialHit(item.Hits)));
            }
            return tmpWinningAnalysis;
        }
    }
}
