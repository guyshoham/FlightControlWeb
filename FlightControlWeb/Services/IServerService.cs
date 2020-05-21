using FlightControlWeb.Models;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace FlightControlWeb.Services
{
    public interface IServerService
    {
        Server AddServer(Server item);
        List<Server> GetServers();
        Server DeleteServerById(string id);
    }
}
