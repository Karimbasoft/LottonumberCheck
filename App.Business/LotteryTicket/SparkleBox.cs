using App.Business.LotteryTicket;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace TestApp.Business
{
    public class SparkleBox
    {
        #region Fields
        private string sparkleBox;
        #endregion

        public SparkleBox()
        {

        }

        public SparkleBox(params int[] numbers) => SparkleBoxNumbers = ConvertIntLottoNumberArrayToString(numbers);

        #region Propertys
        public string SparkleBoxNumbers
        {
            get
            {
                return sparkleBox;
            }
            set
            {
                sparkleBox = value;
            }
        }
        #endregion

        #region Methods
        private string ConvertIntLottoNumberArrayToString(int[] lottoNumbers)
        {
            string tempNumber;

            if (lottoNumbers.Length != 6)
            {
                tempNumber = "";
            }
            else
            {
                tempNumber = string.Join(" ", lottoNumbers);
            }

            return tempNumber;
        }

        /// <summary>
        /// Convertiert eine Collection aus Lottozahlen in einen String
        /// </summary>
        /// <param name="lottoNumbers">Collection aus Lottozahlen</param>
        /// <returns>String mit Lottozahlen</returns>
        public static string ConvertTicketNumberCollectionToString(ObservableCollection<LottoNumber> lottoNumbers)
        {
            string tmp = "";
            int counter = 0;

            if (lottoNumbers.Count == 6)
            {
                foreach (var item in lottoNumbers)
                {
                    counter++;
                    if (counter == 6)
                    {
                        tmp += $"{item.Number}";
                    }
                    else
                    {
                        tmp += $"{item.Number} ";
                    }
                }
            }
            return tmp;
        }
        #endregion
    }
}
