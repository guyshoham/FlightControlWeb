using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        [JsonProperty(PropertyName = "flight_id")]
        public string FlightPlanId { get; set; }

        [JsonProperty(PropertyName = "passengers")]
        public int Passengers { get; set; }

        [JsonProperty(PropertyName = "company_name")]
        public string CompanyName { get; set; }

        [JsonProperty(PropertyName = "initial_location")]
        public InitialLocation InitialLocation { get; set; }

        [JsonProperty(PropertyName = "segments")]
        public Segment[] Segments { get; set; }
    }
}
