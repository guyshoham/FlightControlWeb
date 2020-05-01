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
    public class FlightController : ControllerBase
    {
        private readonly Services.IFlightServices _services;

        public FlightController(Services.IFlightServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("AddFlightItems")]
        public ActionResult<Models.FlightItems> AddFlightItems(Models.FlightItems items)
        {
            var flightItems = _services.AddFlightItems(items);

            if (flightItems == null)
            {
                return NotFound();
            }

            return Ok(flightItems);
        }

        [HttpGet]
        [Route("GetFlightItems")]
        public ActionResult<Dictionary<string, Models.FlightItems>> GetFlightItems()
        {
            var flightItems = _services.GetFlightItems();

            if (flightItems.Count == 0)
            {
                return NotFound();
            }

            return flightItems;
        }

        [HttpDelete]
        [Route("Flights/{id}")]
        public ActionResult<Models.FlightItems> DeleteFlightItems(string id)
        {
            var flightItems = _services.DeleteFlightItems(id);

            if (flightItems == null)
            {
                return NotFound();
            }

            return Ok(flightItems);
        }
    }
}