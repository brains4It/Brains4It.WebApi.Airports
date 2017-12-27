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

            EsCountry country = jsonObject[EsConstants.Country].Value<EsCountry>();
            GeoCoordinate coordinate = jsonObject[EsConstants.Coordinates].Value<GeoCoordinate>();
            Airport result = new Airport
            {
                Id = jsonObject[EsConstants.Id].Value<string>(),
                Type = ConvertType(jsonObject[EsConstants.Type].Value<string>()),
                Name = jsonObject[EsConstants.Id].Value<string>(),
                Elevation = jsonObject[EsConstants.Elevation].Value<double>(),
                Location = new Location
                {
                    Continent = jsonObject[EsConstants.Continent].Value<string>(),
                    CountryName = country.name,
                    IsoCode = country.iso_code,
                    Municipality = jsonObject[EsConstants.Municipality].Value<string>(),
                    Region = jsonObject[EsConstants.Region].Value<string>(),
                },
                GpsCode = jsonObject[EsConstants.GpsCode].Value<string>(),
                IataCode = jsonObject[EsConstants.IataCode].Value<string>(),
                LocalCode = jsonObject[EsConstants.LocalCode].Value<string>(),
                Point = new Point
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
