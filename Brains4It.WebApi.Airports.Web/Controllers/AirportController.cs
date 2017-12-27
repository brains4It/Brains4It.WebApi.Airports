using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Brains4It.WebApi.Airports.Managers;
using Brains4It.WebApi.Airports.Request;

namespace Brains4It.WebApi.Airports.Web.Controllers
{
    [Route("api/airport")]
    public class AirportController : ControllerBase
    {
        private readonly IAirportManager manager;
        public AirportController(IAirportManager manager)
        {
            this.manager = manager;
        }

        [HttpGet]
        [Route("alltypes")]
        public IActionResult GetAirportTypes()
        {
            return Ok(manager.GetAirportTypes());
        }

        [HttpGet]
        [Route("byid/{id}")]
        public IActionResult GetAirportById(string id)
        {
            return Ok(new Brains4It.WebApi.Airports.Models.Airport());
        }

        // POST api/values
        [HttpPost]
        [Route("search")]
        public IActionResult Search([FromBody]AirportRequest request)
        {
            if (!ModelState.IsValid || request == null)
            {
                return BadRequest(ModelState);
            }
            
            var results = manager.GetAirport(request);
            return Ok(results);
        }
    }
}
