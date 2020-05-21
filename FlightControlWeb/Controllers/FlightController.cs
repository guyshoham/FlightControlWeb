using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
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

        // GET api/Flights?relative_to=<DATE_TIME>
        [HttpGet]
        [Route("Flights")]
        public ActionResult<List<Flight>> GetFlightsByTime([FromQuery] DateTime relative_to)
        {
            List<Flight> retVal = _service.GetAllFlightsRelativeToDate(relative_to);

            return Ok(retVal);
        }
    }
}