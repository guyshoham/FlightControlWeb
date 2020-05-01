using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightControlWeb.Models;
using FlightControlWeb.Services;

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

        [HttpPost]
        [Route("AddFlight")]
        public ActionResult<Flight> AddFlight(Flight item)
        {
            var flight = _service.AddFlight(item);

            if (flight == null)
            {
                return NotFound();
            }

            return Ok(flight);
        }

        [HttpGet]
        [Route("GetFlights")]
        public ActionResult<Dictionary<string, Flight>> GetFlights()
        {
            var flight = _service.GetFlights();

            if (flight.Count == 0)
            {
                return NotFound();
            }

            return flight;
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