using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text;

namespace App.Business.Converter
{
    public static class MoneyConverter
    {
        private const string UrlPattern = "http://rate-exchange-1.appspot.com/currency?from={0}&to={1}";

        /// <summary>
        /// Wandelt ein Double in Euros um
        /// </summary>
        /// <param name="doubleValue"></param>
        public static string DoubleToEuros(double doubleValue)
        {
            return doubleValue.ToString("C", CultureInfo.CreateSpecificCulture("de-DE"));
        }

        /// <summary>
        /// Wandelt ein Double in Euros um
        /// </summary>
        /// <param name="doubleValue"></param>
        public static string DoubleToEuros(decimal doubleValue)
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

        /// <summary>
        /// Wandelt Cent-Betrag in Euro um
        /// </summary>
        /// <param name="doubleValue"></param>
        public static decimal CentToEuros(decimal value)
        {
            return (value != 0) ? value / 100 : 0;
        }

        /// <summary>
        /// Wandelt Cent-Betrag in Euro um
        /// </summary>
        /// <param name="doubleValue"></param>
        public static decimal CentToEuros(string value, decimal defaultValue = -1)
        {
            return decimal.TryParse(value, out decimal euroValue)
                ? (euroValue != 0) ? euroValue / 100 : 0
                : defaultValue;
        }


        public static string CurrencyConversion(decimal amount, string fromCurrency, string toCurrency)
        {
            string url = string.Format(UrlPattern, fromCurrency, toCurrency);

            using (var wc = new WebClient())
            {
                var json = wc.DownloadString(url);

                Newtonsoft.Json.Linq.JToken token = Newtonsoft.Json.Linq.JObject.Parse(json);
                decimal exchangeRate = (decimal)token.SelectToken("rate");

                return (amount * exchangeRate).ToString();
            }
        }
    }
}
