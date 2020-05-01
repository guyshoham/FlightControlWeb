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

        public Dictionary<string, FlightItems> GetFlightItems()
        {
            return _flightItems;
        }
    }
}
