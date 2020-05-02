using FlightControlWeb.Models;
using System;
using System.Collections.Generic;

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
            var rand = new Random();
            string id = Utils.IDGenerator();

            while (_servers.ContainsKey(id))
            {
                id = Utils.IDGenerator();
            }

            item.ServerId = id;
            _servers.Add(item.ServerId, item);
            return item;
        }
        public Server DeleteServerById(string id)
        {

            if (!_servers.TryGetValue(id, out Server value))
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
