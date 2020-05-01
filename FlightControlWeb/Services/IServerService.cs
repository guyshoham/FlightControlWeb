using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;


namespace FlightControlWeb.Services
{
    public interface IServerService
    {
        Server AddServer(Server item);
        Dictionary<string, Server> GetServers();
        Server DeleteServerById(string id);
    }
}
