using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.WebApi.Airports
{
    public interface IDataAccess
    {
        IElasticClient AirportElasticClient { get; }
    }
}
