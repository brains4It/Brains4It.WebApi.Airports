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

        public Point Point { get; set; }

        public double Elevation { get; set; }

        public Location Location { get; set; }

        public string GpsCode { get; set; }

        public string IataCode { get; set; }

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
        public string Continent { get; set; }

        public string IsoCode { get; set; }

        public string CountryName { get; set; }

        public string Region { get; set; }

        public string Municipality { get; set; }

    }

}
