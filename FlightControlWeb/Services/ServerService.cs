using FlightControlWeb.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace FlightControlWeb.Services
{
    public class ServerService : IServerService
    {
        private static ConcurrentDictionary<string, Server> _servers;

        public ServerService()
        {
            _servers = new ConcurrentDictionary<string, Server>();
        }

        public Server AddServer(Server item)
        {
            string id = Utils.IDGenerator();

            while (_servers.ContainsKey(id))
            {
                id = Utils.IDGenerator();
            }

            item.ServerId = id;
            _servers.TryAdd(item.ServerId, item);
            return item;
        }
        public Server DeleteServerById(string id)
        {
            _servers.TryRemove(id, out Server value);
            return value;
        }
        public List<Server> GetServers()
        {
            List<Server> list = new List<Server>();

            foreach (Server server in _servers.Values)
            {
                list.Add(server);
            }
            return list;
        }
    }
}
