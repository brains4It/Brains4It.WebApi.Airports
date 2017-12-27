using Brains4It.WebApi.Airports.Request;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Brains4It.WebApi.Airports.Elastic.Helpers
{
    public class EsHelpers
    {

        public static string[] GenerateFields(AirportRequest request)
        {
            List<string> results = new List<string>
            {
                EsConstants.Id,
                EsConstants.Name,
                EsConstants.Type
            };

            if (request?.Attributes.HasValue ?? false)
            {

                if (request.Attributes.Value.HasFlag(AirportAttributes.Coordinates))
                {
                    results.Add(EsConstants.Coordinates);
                }

                if (request.Attributes.Value.HasFlag(AirportAttributes.Elevation))
                {
                    results.Add(EsConstants.Coordinates);
                }

                if (request.Attributes.Value.HasFlag(AirportAttributes.Location))
                {
                    results.Add(EsConstants.Continent);
                    results.Add(EsConstants.Country);
                    results.Add(EsConstants.Municipality);
                    results.Add(EsConstants.Region);
                }

                if (request.Attributes.Value.HasFlag(AirportAttributes.GpsCode))
                {
                    results.Add(EsConstants.GpsCode);
                }

                if (request.Attributes.Value.HasFlag(AirportAttributes.IataCode))
                {
                    results.Add(EsConstants.IataCode);
                }

                if (request.Attributes.Value.HasFlag(AirportAttributes.LocalCode))
                {
                    results.Add(EsConstants.LocalCode);
                }
            }
            

            return results.ToArray();

        }

        public static Func<QueryContainerDescriptor<EsAirport>, QueryContainer>[] QueryGenerator(AirportRequest request)
        {
            List<Func<QueryContainerDescriptor<EsAirport>, QueryContainer>> results = new List<Func<QueryContainerDescriptor<EsAirport>, QueryContainer>>();

            if (request.Ids?.Any() ?? false)
            {
                Func<QueryContainerDescriptor<EsAirport>, QueryContainer> funcIds = tmp =>
                    tmp.Terms(m => m.Field(f => f.id).Terms(request.Ids));
                results.Add(funcIds);
            }

            if (request.Types?.Any() ?? false)
            {
                Func<QueryContainerDescriptor<EsAirport>, QueryContainer> funcZoneType = tmp =>
                    tmp.Terms(m => m.Field(f => f.type).Terms(request.Types));

                results.Add(funcZoneType);
            }

            //if (!string.IsNullOrEmpty(request.IsoCode))
            //{
            //    Func<QueryContainerDescriptor<EsAirport>, QueryContainer> funcIsoId = tmp =>
            //        tmp.Term(m => m.IsoId, request.IsoCode);

            //    results.Add(funcIsoId);
            //}


            if (!string.IsNullOrEmpty(request.Name))
            {
                Func<QueryContainerDescriptor<EsAirport>, QueryContainer> funcName = tmp =>
                    tmp.Term(m => m.name, request.Name);
                results.Add(funcName);
            }


            if (!string.IsNullOrEmpty(request.IataCode))
            {
                Func<QueryContainerDescriptor<EsAirport>, QueryContainer> funcName = tmp =>
                    tmp.Term(m => m.iata_code, request.IataCode);
                results.Add(funcName);
            }
            

            if (request.Geometry != null)
            {
                Func<QueryContainerDescriptor<EsAirport>, QueryContainer> funcPolygon = tmp =>
                       tmp.GeoBoundingBox(g => g
                           .Field(f => f.coordinates)
                           .BoundingBox(request.Geometry.LatMax, request.Geometry.LngMin, request.Geometry.LatMin, request.Geometry.LngMax));

                results.Add(funcPolygon);
            }

            return results.ToArray();
        }
    }
}
