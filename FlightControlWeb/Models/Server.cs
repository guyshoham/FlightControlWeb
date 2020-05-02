using Newtonsoft.Json;

namespace FlightControlWeb.Models
{
    public class Server
    {
        [JsonProperty(PropertyName = "ServerId")]
        public string ServerId { get; set; }

        [JsonProperty(PropertyName = "ServerURL")]
        public string ServerURL { get; set; }
    }
}
