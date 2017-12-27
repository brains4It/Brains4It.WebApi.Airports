using Nest;
using System;

namespace Brains4It.WebApi.Airports
{
    public class DataAccess : IDataAccess
    {
        private readonly IElasticClient airportElasticClient;
        public DataAccess(IElasticClient airportElasticClient)
        {
            this.airportElasticClient = airportElasticClient;
        }

        public IElasticClient AirportElasticClient
        {
            get { return airportElasticClient; }
        }

    }
}
