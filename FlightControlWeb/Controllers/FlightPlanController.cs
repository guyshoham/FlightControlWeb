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
    public class FlightPlanController : ControllerBase
    {
        private readonly IFlightPlanService _service;

        public FlightPlanController(IFlightPlanService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("FlightPlan")]
        public ActionResult<FlightPlan> AddFlightPlan(JsonElement json)
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
        public ActionResult<Dictionary<string, FlightPlan>> GetFlightPlanById(string id)
        {
            var flightPlan = _service.GetFlightPlanById(id);

            if (flightPlan == null)
            {
                return NotFound();
            }

            return Ok(flightPlan);
        }
    }
}