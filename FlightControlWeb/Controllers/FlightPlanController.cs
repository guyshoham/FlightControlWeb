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
    public class FlightPlanController : ControllerBase
    {
        private readonly IFlightPlanService _service;

        public FlightPlanController(IFlightPlanService service)
        {
            _service = service;
        }

        [HttpPost]
        [Route("AddFlightPlan")]
        public ActionResult<FlightPlan> AddFlightPlan(FlightPlan item)
        {
            var flightPlan = _service.AddFlightPlan(item);

            if (flightPlan == null)
            {
                return NotFound();
            }

            return Ok(flightPlan);
        }

        [HttpGet]
        [Route("GetFlightPlans")]
        public ActionResult<Dictionary<string, FlightPlan>> GetFlightPlans()
        {
            var flightPlan = _service.GetFlightPlans();

            if (flightPlan.Count == 0)
            {
                return NotFound();
            }

            return flightPlan;
        }
    }
}