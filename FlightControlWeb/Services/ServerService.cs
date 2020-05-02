using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public class ServerService : IServerService
    {
        private readonly Dictionary<string, Server> _servers;

        public ServerService()
        {
            _servers = new Dictionary<string, Server>();
        }

        public Server AddServer(Server item)
        {
            if (_servers.ContainsKey(item.ServerId))
            {
                //the key is already exist, return an object with id="-1"
                Server s = new Server
                {
                    ServerId = "-1"
                };
                return s;
            }
            _servers.Add(item.ServerId, item);
            return item;
        }

        public Server DeleteServerById(string id)
        {
            Server value;

            if (!_servers.TryGetValue(id, out value))
            {
                // the key isn't in the dictionary.
                return null;
            }

            bool status = _servers.Remove(id);

            return status ? value : null;
        }

        public Dictionary<string, Server> GetServers()
        {
            return _servers;
        }
    }
}
