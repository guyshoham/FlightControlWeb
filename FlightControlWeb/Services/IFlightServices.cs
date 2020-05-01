using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public interface IFlightServices
    {
        FlightItems AddFlightItems(FlightItems items);
        Dictionary<string, FlightItems> GetFlightItems();
        FlightItems DeleteFlightItems(string id);
    }
}
