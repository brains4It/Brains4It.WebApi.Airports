using System;
using System.Collections.Generic;
using System.Text;
using Brains4It.WebApi.Airports.Elastic;
using Brains4It.WebApi.Airports.Elastic.Converters;
using Brains4It.WebApi.Airports.Elastic.Helpers;
using Brains4It.WebApi.Airports.Models;
using Brains4It.WebApi.Airports.Request;
using Nest;

namespace Brains4It.WebApi.Airports.Managers
{
    public class AirportManager : IAirportManager
    {
        #region Fields
        private readonly IDataAccess dataAccess;
        #endregion

        #region Constructors
        public AirportManager(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }
        #endregion

        #region Public Methods
        public IEnumerable<Airport> GetAirport(AirportRequest request)
        {
            var searchCriterias = new SearchDescriptor<EsAirport>();
            searchCriterias
                .From(request.StartingIndex)
                .Size(request.ResponseSize)
                .Source(src => src.Includes(i => i.Fields(EsHelpers.GenerateFields(request))))
                .Query(q => q.Bool(b => b.Must(EsHelpers.QueryGenerator(request))));

            var jsonResponse = dataAccess.AirportElasticClient.Search<object>(searchCriterias);
            
            return CreateAirportResponse(jsonResponse.Documents);
        }

        public IEnumerable<string> GetAirportTypes()
        {
            return Enum.GetNames(typeof(Models.AirportTypeEnum));
        }
        #endregion

        #region Private Methods

        private IEnumerable<Airport> CreateAirportResponse(IReadOnlyCollection<object> documents)
        {
            foreach(var doc in documents)
            {
                yield return Newtonsoft.Json.JsonConvert.DeserializeObject<Airport>(doc.ToString(), new AirportJsonConverter(), new CountryJsonConverter());
            }
        }
        #endregion
    }
}
