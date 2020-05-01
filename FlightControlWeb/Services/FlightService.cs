using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public class FlightService : IFlightService
    {

        private readonly Dictionary<string, Flight> _flights;

        public FlightService()
        {
            _flights = new Dictionary<string, Flight>();
        }

        public Flight AddFlight(Flight item)
        {
            _flights.Add(item.flight_id, item);

            return item;
        }

        public Flight DeleteFlightById(string id)
        {
            Flight value;

            if (!_flights.TryGetValue(id, out value))
            {
                // the key isn't in the dictionary.
                return null; // or whatever you want to do
            }

            bool status = _flights.Remove(id);

            return status ? value : null;
        }

        public Dictionary<string, Flight> GetFlights()
        {
            return _flights;
        }
    }
}
