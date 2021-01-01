using App.Business.LotteryTicket;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Business.Converter
{
    internal class LotteryWinningNumberElementConverter : JsonConverter
    {
        public override bool CanConvert(Type t) => t == typeof(LotteryWinningNumbersElement) || t == typeof(LotteryWinningNumbersElement?);

        public override object ReadJson(JsonReader reader, Type t, object existingValue, JsonSerializer serializer)
        {
            switch (reader.TokenType)
            {
                case JsonToken.String:
                case JsonToken.Date:
                    var stringValue = serializer.Deserialize<string>(reader);
                    return new LotteryWinningNumbersElement { String = stringValue };
                case JsonToken.StartObject:
                    var objectValue = serializer.Deserialize<LotteryWinningNumbers>(reader);
                    return new LotteryWinningNumbersElement { TemperatureClass = objectValue };
            }
            throw new Exception("Cannot unmarshal type TemperatureElement");
        }

        public override void WriteJson(JsonWriter writer, object untypedValue, JsonSerializer serializer)
        {
            var value = (LotteryWinningNumbersElement)untypedValue;
            if (value.String != null)
            {
                serializer.Serialize(writer, value.String);
                return;
            }
            if (value.TemperatureClass != null)
            {
                serializer.Serialize(writer, value.TemperatureClass);
                return;
            }
            throw new Exception("Cannot marshal type TemperatureElement");
        }

        public static readonly LotteryWinningNumberElementConverter Singleton = new LotteryWinningNumberElementConverter();
    }
}
