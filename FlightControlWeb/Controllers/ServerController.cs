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
        private readonly IServerService _service;

        public ServerController(IServerService services)
        {
            _service = services;
        }

        [HttpPost]
        [Route("servers")]
        public ActionResult<Server> AddServer(Server item)
        {
            var server = _service.AddServer(item);

            if (server == null)
            {
                return NotFound();
            }

            return Ok(server);
        }

        [HttpGet]
        [Route("servers")]
        public ActionResult<Dictionary<string, Server>> GetServers()
        {
            var server = _service.GetServers();

            if (server.Count == 0)
            {
                return NotFound();
            }

            return server;
        }

        [HttpDelete]
        [Route("servers/{id}")]
        public ActionResult<Flight> DeleteServerById(string id)
        {
            var server = _service.DeleteServerById(id);

            if (server == null)
            {
                return NotFound();
            }

            return Ok(server);
        }
    }
}