using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public class FlightPlanServices : IFlightPlanServices
    {

        private readonly Dictionary<string, FlightPlanItems> _flightPlanItems;

        public FlightPlanServices()
        {
            _flightPlanItems = new Dictionary<string, FlightPlanItems>();
        }

        public FlightPlanItems AddFlightPlanItems(FlightPlanItems items)
        {
            _flightPlanItems.Add(items.flight_id, items);

            return items;
        }

        public Dictionary<string, FlightPlanItems> GetFlightPlanItems()
        {
            return _flightPlanItems;
        }
    }
}
