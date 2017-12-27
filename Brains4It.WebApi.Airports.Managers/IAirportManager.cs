using Brains4It.WebApi.Airports.Models;
using Brains4It.WebApi.Airports.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.WebApi.Airports.Managers
{
    public interface IAirportManager
    {
        IEnumerable<Airport> GetAirport(AirportRequest request);

        IEnumerable<string> GetAirportTypes();
    }
}
