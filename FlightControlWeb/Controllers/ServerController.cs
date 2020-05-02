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
    public class ServerController : ControllerBase
    {
        private readonly IServerService _service;

        public ServerController(IServerService services)
        {
            _service = services;
        }

        [HttpPost]
        [Route("servers")]
        public ActionResult<Server> AddServer(JsonElement json)
        {
            Server serializedJson = JsonConvert.DeserializeObject<Server>(json.ToString());
            Server server = _service.AddServer(serializedJson);

            if (server == null)
            {
                return NotFound();
            }

            if (server.ServerId == "-1")
            {
                // the key is already exist, return response code 409: conflict
                return Conflict();
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