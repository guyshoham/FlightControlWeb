using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public class FlightServices : IFlightServices
    {

        private readonly Dictionary<string, FlightItems> _flightItems;

        public FlightServices()
        {
            _flightItems = new Dictionary<string, FlightItems>();
        }

        public FlightItems AddFlightItems(FlightItems items)
        {
            _flightItems.Add(items.flight_id, items);

            return items;
        }

        public FlightItems DeleteFlightItems(string id)
        {
            FlightItems value;

            if (!_flightItems.TryGetValue(id, out value))
            {
                // the key isn't in the dictionary.
                return null; // or whatever you want to do
            }

            bool status = _flightItems.Remove(id);

            return status ? value : null;
        }

        public Dictionary<string, FlightItems> GetFlightItems()
        {
            return _flightItems;
        }
    }
}
