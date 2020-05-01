using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FlightControlWeb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class FlightPlanController : ControllerBase
    {
        private readonly Services.IFlightPlanServices _services;

        public FlightPlanController(Services.IFlightPlanServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("AddFlightPlanItems")]
        public ActionResult<Models.FlightPlanItems> AddFlightPlanItems(Models.FlightPlanItems items)
        {
            var flightPlanItems = _services.AddFlightPlanItems(items);

            if (flightPlanItems == null)
            {
                return NotFound();
            }

            return Ok(flightPlanItems);
        }

        [HttpGet]
        [Route("GetFlightPlanItems")]
        public ActionResult<Dictionary<string, Models.FlightPlanItems>> GetFlightPlanItems()
        {
            var flightPlanItems = _services.GetFlightPlanItems();

            if (flightPlanItems.Count == 0)
            {
                return NotFound();
            }

            return flightPlanItems;
        }
    }
}