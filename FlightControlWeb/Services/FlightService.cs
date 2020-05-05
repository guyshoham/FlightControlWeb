using FlightControlWeb.Models;
using System.Collections.Generic;

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
            _flights.Add(item.FlightId, item);

            return item;
        }
        public Flight DeleteFlightById(string id)
        {

            if (!_flights.TryGetValue(id, out Flight value))
            {
                // the key isn't in the dictionary.
                return null;
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
