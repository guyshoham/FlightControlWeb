using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FlightControlWeb.Services;
using FlightControlWeb.Models;

namespace FlightControlWeb.Controllers
{
    [Route("api/")]
    [ApiController]
    public class ServerController : ControllerBase
    {
        private readonly IServerServices _services;

        public ServerController(IServerServices services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("servers")]
        public ActionResult<ServerItems> AddServerItems(ServerItems items)
        {
            //TODO: implement
            return NotFound();
        }

        [HttpGet]
        [Route("servers")]
        public ActionResult<Dictionary<string, ServerItems>> GetServerItems()
        {
            //TODO: implement
            return NotFound();
        }

        [HttpDelete]
        [Route("servers/{id}")]
        public ActionResult<FlightItems> DeleteServerItems(string id)
        {
            //TODO: implement
            return NotFound();
        }
    }
}