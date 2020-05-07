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
    public class FlightPlanController : ControllerBase
    {
        private readonly IFlightService _service;

        public FlightPlanController(IFlightService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("FlightPlan")]
        public ActionResult<FlightPlan> Post(JsonElement json)
        {
            FlightPlan serializedJson = JsonConvert.DeserializeObject<FlightPlan>(json.ToString());
            FlightPlan flightPlan = _service.AddFlightPlan(serializedJson);

            if (flightPlan == null)
            {
                return NotFound();
            }

            return Ok(flightPlan);
        }

        [HttpGet]
        [Route("FlightPlan/{id}")]
        public ActionResult<Dictionary<string, FlightPlan>> Get(string id)
        {
            var flightPlan = _service.GetFlightPlanById(id);

            if (flightPlan == null)
            {
                return NotFound();
            }

            return Ok(flightPlan);
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        public ActionResult<Flight> Delete(string id)
        {
            FlightPlan flightPlan = _service.DeleteFlightPlanById(id);

            if (flightPlan == null)
            {
                return NotFound();
            }

            return Ok(flightPlan);
        }
    }
}