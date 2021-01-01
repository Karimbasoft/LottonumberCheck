using App.Business.Converter;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Web
{
    public partial class LottolandApiResult
    {
        [JsonProperty("last")]
        public Last Last { get; set; }

        [JsonProperty("next")]
        public Next Next { get; set; }
    }

    public partial class Last
    {
        [JsonProperty("nr")]
        public long Nr { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("date")]
        public Date Date { get; set; }

        [JsonProperty("closingDate")]
        public string ClosingDate { get; set; }

        [JsonProperty("lateClosingDate")]
        public string LateClosingDate { get; set; }

        [JsonProperty("drawingDate")]
        public string DrawingDate { get; set; }

        [JsonProperty("numbers")]
        public List<long> Numbers { get; set; }

        [JsonProperty("zusatzzahl")]
        public int Zusatzzahl { get; set; }

        [JsonProperty("superzahl")]
        public int Superzahl { get; set; }

        [JsonProperty("super6")]
        public string Super6 { get; set; }

        [JsonProperty("spiel77")]
        public string Spiel77 { get; set; }

        [JsonProperty("jackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Jackpot { get; set; }

        [JsonProperty("marketingJackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MarketingJackpot { get; set; }

        [JsonProperty("specialMarketingJackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SpecialMarketingJackpot { get; set; }

        [JsonProperty("climbedSince")]
        public long ClimbedSince { get; set; }

        [JsonProperty("Winners")]
        public long Winners { get; set; }

        [JsonProperty("odds")]
        public Dictionary<string, Odd> Odds { get; set; }

        [JsonProperty("spiel77jackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Spiel77Jackpot { get; set; }

        [JsonProperty("spiel77Winners")]
        public long Spiel77Winners { get; set; }

        [JsonProperty("spiel77Odds")]
        public Dictionary<string, Odd> Spiel77Odds { get; set; }

        [JsonProperty("super6Winners")]
        public long Super6Winners { get; set; }

        [JsonProperty("super6Odds")]
        public Dictionary<string, Odd> Super6Odds { get; set; }
    }

    public partial class Date
    {
        [JsonProperty("full")]
        public string Full { get; set; }

        [JsonProperty("day")]
        public long Day { get; set; }

        [JsonProperty("month")]
        public long Month { get; set; }

        [JsonProperty("year")]
        public long Year { get; set; }

        [JsonProperty("hour")]
        public long Hour { get; set; }

        [JsonProperty("minute")]
        public long Minute { get; set; }

        [JsonProperty("dayOfWeek")]
        public string DayOfWeek { get; set; }
    }

    public partial class Odd
    {
        [JsonProperty("winners")]
        public int Winners { get; set; }

        [JsonProperty("specialPrize")]
        public int SpecialPrize { get; set; }

        [JsonProperty("prize")]
        public decimal Prize { get; set; }
    }

    public partial class Next
    {
        [JsonProperty("nr")]
        public long Nr { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("date")]
        public Date Date { get; set; }

        [JsonProperty("closingDate")]
        public string ClosingDate { get; set; }

        [JsonProperty("lateClosingDate")]
        public string LateClosingDate { get; set; }

        [JsonProperty("drawingDate")]
        public string DrawingDate { get; set; }

        [JsonProperty("jackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Jackpot { get; set; }

        [JsonProperty("marketingJackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long MarketingJackpot { get; set; }

        [JsonProperty("specialMarketingJackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long SpecialMarketingJackpot { get; set; }

        [JsonProperty("climbedSince")]
        public long ClimbedSince { get; set; }

        [JsonProperty("spiel77jackpot")]
        [JsonConverter(typeof(ParseStringConverter))]
        public long Spiel77Jackpot { get; set; }
    }
}

