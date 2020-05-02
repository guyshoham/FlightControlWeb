using FlightControlWeb.Models;
using System.Collections.Generic;

namespace FlightControlWeb.Services
{
    public interface IFlightService
    {
        Flight AddFlight(Flight item);
        Dictionary<string, Flight> GetFlights();
        Flight DeleteFlightById(string id);
    }
}
