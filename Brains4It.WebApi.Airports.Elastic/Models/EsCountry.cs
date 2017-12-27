using Nest;
using System;
using System.Collections.Generic;
using System.Text;

namespace Brains4It.WebApi.Airports.Elastic
{
    [ElasticsearchType(Name = "airport")]
    public class EsCountry
    {
        [Keyword(Name = "iso_code")]
        public string iso_code { get; set; }
        [Text(Name = "name")]
        public string name { get; set; }

    }
}
