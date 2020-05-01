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
        [Route("FlightPlan")]
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