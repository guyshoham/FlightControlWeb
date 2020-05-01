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
        private readonly IServerService _services;

        public ServerController(IServerService services)
        {
            _services = services;
        }

        [HttpPost]
        [Route("servers")]
        public ActionResult<Server> AddServer(Server item)
        {
            //TODO: implement
            return NotFound();
        }

        [HttpGet]
        [Route("servers")]
        public ActionResult<Dictionary<string, Server>> GetServers()
        {
            //TODO: implement
            return NotFound();
        }

        [HttpDelete]
        [Route("servers/{id}")]
        public ActionResult<Flight> DeleteServerById(string id)
        {
            //TODO: implement
            return NotFound();
        }
    }
}