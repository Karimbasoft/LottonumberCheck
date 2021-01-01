using App.Business.LotteryTicket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Web
{
    public class HubobelAPIResult
    {
        [JsonProperty("")]
        public string Hinweis { get; set; }

        public LotteryWinningNumbers LotteryWinningNumbers {get;set;}
    }
}
