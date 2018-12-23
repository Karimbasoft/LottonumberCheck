using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace App.Business.Converter
{
    public static class MoneyConverter
    {
        /// <summary>
        /// Wandelt ein Double in Euros um
        /// </summary>
        /// <param name="doubleValue"></param>
        public static string DoubleToEuros(double doubleValue)
        {
            return doubleValue.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"));
        }

        /// <summary>
        /// Wandelt Cent-Betrag in Euro um
        /// </summary>
        /// <param name="doubleValue"></param>
        public static double CentToEuros(double doubleValue)
        {
            return (doubleValue != 0) ? doubleValue / 100 : 0;
        }
    }
}
