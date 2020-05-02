using FlightControlWeb.Models;
using System.Collections.Generic;


namespace FlightControlWeb.Services
{
    public interface IServerService
    {
        Server AddServer(Server item);
        Dictionary<string, Server> GetServers();
        Server DeleteServerById(string id);
    }
}
