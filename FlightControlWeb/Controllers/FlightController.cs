using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace FlightControlWeb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _service;
        static HttpClient client = new HttpClient();

        public FlightController(IFlightService service)
        {
            _service = service;
        }

        // GET api/Flights?relative_to=<DATE_TIME>&sync_all (sync_all=OPTIONAL)
        [HttpGet]
        [Route("Flights")]
        public async Task<ActionResult<string>> GetFlightsByTimeAsync([FromQuery(Name = "relative_to")] DateTime relativeTo, [Optional] string sync_all)
        {
            List<Flight> allFlights = new List<Flight>();
            List<Flight> externalsFlights = new List<Flight>();
            List<Flight> internalFlights = _service.GetAllFlightsRelativeToDate(relativeTo);

            string request = Request.QueryString.Value;
            bool syncAll = request.Contains("sync_all");
            if (syncAll)
            {
                var serversResponse = await GetServers();
                var serversBody = serversResponse.Content.ReadAsStringAsync().Result;
                List<Server> servers = JsonConvert.DeserializeObject<List<Server>>(serversBody);

                foreach (Server server in servers)
                {
                    var flightsResponse = await GetFlightsFromAnotherServer(server.ServerURL, relativeTo);
                    if (flightsResponse.StatusCode == HttpStatusCode.OK)
                    {
                        var flightsBody = flightsResponse.Content.ReadAsStringAsync().Result;
                        List<Flight> externals_one_server = JsonConvert.DeserializeObject<List<Flight>>(flightsBody); // get list of all flights from one external server

                        foreach (Flight flight in externals_one_server)
                        {
                            flight.From = server.ServerURL;
                        }

                        externalsFlights.AddRange(externals_one_server); // unite this list with all external flights list
                    }
                }

                //change all external flights to isExternal=true
                foreach (Flight flight in externalsFlights)
                {
                    flight.IsExternal = true;
                }

                allFlights.AddRange(internalFlights);
                allFlights.AddRange(externalsFlights);
                string retVal = JsonConvert.SerializeObject(allFlights);
                return retVal;
            }
            else
            {
                string retVal = JsonConvert.SerializeObject(internalFlights);
                return retVal;
            }
        }

        async Task<HttpResponseMessage> GetServers()
        {
            var baseAddr = Request.Host.Value;

            string url = "http://" + baseAddr + "/api/servers";

            HttpResponseMessage response = await client.GetAsync(url);
            return response;
        }

        async Task<HttpResponseMessage> GetFlightsFromAnotherServer(string serverUrl, DateTime relative_to)
        {
            string url = serverUrl + "/api/Flights?relative_to=" + relative_to.ToString("yyyy-MM-ddTHH:mm:ssZ");
            HttpResponseMessage response = await client.GetAsync(url);
            return response;
        }
    }
}