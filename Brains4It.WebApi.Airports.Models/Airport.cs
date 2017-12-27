using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Brains4It.WebApi.Airports.Models
{
    public class Airport
    {
        public string Id { get; set; }

        public AirportTypeEnum? Type { get; set; }

        public string Name { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Point Point { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public double? Elevation { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public Location Location { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string GpsCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IataCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string LocalCode { get; set; }
    }

    public enum AirportTypeEnum
    {
        BalloonPort,
        Closed,
        Heliport,
        LargeAirport,
        MediumAirport,
        SeaPlaneBase,
        SmallAirport
    }

    public class Point
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }
    }

    public class Location
    {
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Continent { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string IsoCode { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string CountryName { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Region { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Municipality { get; set; }

    }

}
