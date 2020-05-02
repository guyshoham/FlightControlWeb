using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json;

namespace FlightControlWeb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly IFlightService _service;

        public FlightController(IFlightService service)
        {
            _service = service;
        }

        // TODO: remove this method
        [HttpPost]
        [Route("AddFlight")]
        public ActionResult<Flight> AddFlight(JsonElement json)
        {
            Flight serializedJson = JsonConvert.DeserializeObject<Flight>(json.ToString());
            Flight flight = _service.AddFlight(serializedJson);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        // TODO: remove this method
        [HttpGet]
        [Route("GetFlights")]
        public ActionResult<Dictionary<string, Flight>> GetFlights()
        {
            var flights = _service.GetFlights();

            if (flights.Count == 0)
            {
                return NotFound();
            }

            return Ok(flights);
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        public ActionResult<Flight> DeleteFlightById(string id)
        {
            var flight = _service.DeleteFlightById(id);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }
    }
}