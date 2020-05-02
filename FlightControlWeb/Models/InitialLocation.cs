using Newtonsoft.Json;
using System;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "date_time")]
        public DateTime DateTime { get; set; }
    }
}
