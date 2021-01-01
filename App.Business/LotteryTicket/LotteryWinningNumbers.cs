using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.LotteryTicket
{
    public class LotteryWinningNumbers
    {
        [JsonProperty("Datum")]
        public string Datum { get; set; }

        [JsonProperty("Spiel77")]
        public long Spiel77 { get; set; }

        [JsonProperty("Super6")]
        public long Super6 { get; set; }

        [JsonProperty("Superzahl")]
        public long Superzahl { get; set; }

        [JsonProperty("Z1")]
        public long Z1 { get; set; }

        [JsonProperty("Z2")]
        public long Z2 { get; set; }

        [JsonProperty("Z3")]
        public long Z3 { get; set; }

        [JsonProperty("Z4")]
        public long Z4 { get; set; }

        [JsonProperty("Z5")]
        public long Z5 { get; set; }

        [JsonProperty("Z6")]
        public long Z6 { get; set; }
    }

    public partial struct LotteryWinningNumbersElement
    {
        public string String;
        public LotteryWinningNumbers TemperatureClass;

        public static implicit operator LotteryWinningNumbersElement(string String) => new LotteryWinningNumbersElement { String = String };
        public static implicit operator LotteryWinningNumbersElement(LotteryWinningNumbers TemperatureClass) => new LotteryWinningNumbersElement { TemperatureClass = TemperatureClass };
    }
}
