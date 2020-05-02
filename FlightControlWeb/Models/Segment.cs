using Newtonsoft.Json;
using System;

namespace FlightControlWeb.Models
{
    public class Segment
    {
        [JsonProperty(PropertyName = "longitude")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "timespan_seconds")]
        public int TimespanSeconds { get; set; }
    }
}
