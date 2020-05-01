using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public interface IFlightService
    {
        Flight AddFlight(Flight item);
        Dictionary<string, Flight> GetFlights();
        Flight DeleteFlightById(string id);
    }
}
