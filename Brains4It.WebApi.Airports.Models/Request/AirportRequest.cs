using Brains4It.WebApi.Airports.Models;

namespace Brains4It.WebApi.Airports.Request
{
    public class AirportRequest
    {
        public string[] Ids { get; set; }

        public AirportAttributes? Attributes { get; set; }

        public int StartingIndex { get; set; }

        public int ResponseSize { get; set; }

        public AirportTypeEnum[] Types { get; set; } 

        public string IsoCode { get; set; }

        public string Name { get; set; }

        public string IataCode { get; set; }

        public GeometryCriterias Geometry { get; set; }
    }

    public class GeometryCriterias
    {
        public double LatMin { get; set; }
        public double LatMax { get; set; }
        public double LngMin { get; set; }
        public double LngMax { get; set; }
    }

    public enum AirportAttributes
    {
        Coordinates,
        Elevation,
        Location,
        GpsCode,
        IataCode,
        LocalCode
    }
}
