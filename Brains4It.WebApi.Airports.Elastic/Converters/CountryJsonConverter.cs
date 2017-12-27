using Brains4It.WebApi.Airports.Elastic.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.WebApi.Airports.Elastic.Converters
{
    public class CountryJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(EsCountry);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);
            return new EsCountry
            {
                iso_code = jsonObject[EsConstants.IsoCode].Value<string>(),
                name = jsonObject[EsConstants.Name].Value<string>()
            };
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }
    }
}
