using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

namespace FlightControlWeb.Models
{
    public class InitialLocation
    {
        [JsonProperty(PropertyName = "longitude")]
        [Range(-180, 180, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Longitude { get; set; }

        [JsonProperty(PropertyName = "latitude")]
        [Range(-90, 90, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public double Latitude { get; set; }

        [JsonProperty(PropertyName = "date_time")]
        public DateTime DateTime { get; set; }
    }
}
