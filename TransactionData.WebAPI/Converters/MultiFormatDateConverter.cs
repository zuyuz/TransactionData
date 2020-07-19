using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TransactionData.WebAPI.Converters
{
    public class MultiFormatDateConverter : IsoDateTimeConverter
    {
        public List<string> DateTimeFormats { get; set; }

        public MultiFormatDateConverter()
        {
            DateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ssK";
        }

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(DateTimeOffset);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
                return null;

            var dateString = reader.Value.ToString();
            foreach (string format in DateTimeFormats)
            {
                // adjust this as necessary to fit your needs
                if (DateTimeOffset.TryParseExact(dateString, format,
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.AssumeUniversal, out var dateTimeOffset))
                    return dateTimeOffset;
            }
            throw new JsonException("Unable to parse \"" + dateString + "\" as a date.");
        }
    }
}
