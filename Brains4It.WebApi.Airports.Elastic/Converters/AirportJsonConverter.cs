using Brains4It.WebApi.Airports.Elastic.Helpers;
using Brains4It.WebApi.Airports.Models;
using Nest;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.WebApi.Airports.Elastic.Converters
{
    public class AirportJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => objectType == typeof(Airport);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jsonObject = JObject.Load(reader);

            EsCountry country = jsonObject[EsConstants.Country]?.ToObject<EsCountry>(serializer) ?? null;
            GeoCoordinate coordinate = jsonObject[EsConstants.Coordinates]?.ToObject<GeoCoordinate>(serializer) ?? null;
            Airport result = new Airport
            {
                Id = jsonObject[EsConstants.Id].Value<string>(),
                Type = ConvertType(jsonObject[EsConstants.Type]?.Value<string>()) ?? null,
                Name = jsonObject[EsConstants.Name]?.Value<string>() ?? null,
                Elevation = jsonObject[EsConstants.Elevation]?.Value<double>() ?? null,
                Location = new Location
                {
                    Continent = jsonObject[EsConstants.Continent]?.Value<string>() ?? null,
                    CountryName = country?.name ?? null,
                    IsoCode = country?.iso_code ?? null,
                    Municipality = jsonObject[EsConstants.Municipality]?.Value<string>() ?? null,
                    Region = jsonObject[EsConstants.Region]?.Value<string>() ?? null,
                },
                GpsCode = jsonObject[EsConstants.GpsCode]?.Value<string>() ?? null,
                IataCode = jsonObject[EsConstants.IataCode]?.Value<string>() ?? null,
                LocalCode = jsonObject[EsConstants.LocalCode]?.Value<string>() ?? null,
                Point = coordinate == null ? null : new Point
                {
                    Latitude = coordinate.Latitude,
                    Longitude = coordinate.Longitude
                }
            };

            return result;            
        }


        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }


        private AirportTypeEnum? ConvertType(string type)
        {
            switch(type)
            {
                case "balloonport":
                    return AirportTypeEnum.BalloonPort;
                case "closed":
                    return AirportTypeEnum.Closed;
                case "heliport":
                    return AirportTypeEnum.Heliport;
                case "large_airport":
                    return AirportTypeEnum.LargeAirport;
                case "medium_airport":
                    return AirportTypeEnum.MediumAirport;
                case "seaplane_base":
                    return AirportTypeEnum.SeaPlaneBase;
                case "small_airport":
                    return AirportTypeEnum.SmallAirport;
                default:
                    return null;
            }
        }
    }
}
