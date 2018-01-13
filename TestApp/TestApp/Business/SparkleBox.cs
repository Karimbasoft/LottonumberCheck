using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        #endregion
    }
}
