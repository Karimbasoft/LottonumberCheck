using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Web
{

    public class CSSClasses
    {
        #region Fields

        #endregion

        #region Propertys
        /// <summary>
        /// Beinhaltet die CSSClassen
        /// </summary>
        public Dictionary<string, string> CSSClassDictionary { get; set; }
        #endregion

        #region Enum

        public enum CSSClassNames
        {
            SuperNumber,
            WinningQuotesSpielSiebenundsiebzigStart,
            WinningQuotesSpielSiebenundsiebzigEnd
        }
        #endregion

        public CSSClasses()
        {
            CSSClassDictionary = CreateStandardCSSClasses();
        }

        private Dictionary<string, string> CreateStandardCSSClasses()
        {
            Dictionary<string, string> keyValuePairs = new Dictionary<string, string>
            {
                { CSSClassNames.SuperNumber.ToString(), "class=\"winning-numbers__number winning-numbers__number--superzahl\"" },
                { CSSClassNames.WinningQuotesSpielSiebenundsiebzigStart.ToString(), "class=\"inner-table-header align-middle hidden-xs" },
                { CSSClassNames.WinningQuotesSpielSiebenundsiebzigEnd.ToString(),  "class=\"inner-table-header align-middle hidden-xs" }
            };


            return keyValuePairs;
        }
    }
}
