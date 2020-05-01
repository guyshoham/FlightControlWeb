using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FlightControlWeb.Models;


namespace FlightControlWeb.Services
{
    public interface IServerServices
    {
        ServerItems AddServerItems(ServerItems items);
        Dictionary<string, ServerItems> GetServerItems();
        ServerItems DeleteServerItems(string id);
    }
}
