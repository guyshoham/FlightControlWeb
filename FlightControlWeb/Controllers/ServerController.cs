using FlightControlWeb.Models;
using FlightControlWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Collections.Generic;

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

        // POST api/servers
        [HttpPost]
        [Route("servers")]
        public ActionResult<Server> Post(object json)
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

        // GET api/servers
        [HttpGet]
        [Route("servers")]
        public ActionResult<List<Server>> Get()
        {
            var list = _service.GetServers();

            return list;
        }

        // DELETE api/servers/{id}
        [HttpDelete]
        [Route("servers/{id}")]
        public ActionResult<Server> Delete(string id)
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