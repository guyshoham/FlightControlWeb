using FlightControlWeb.Models;
using System.Collections.Generic;

namespace FlightControlWeb.Services
{
    public class FlightPlanService : IFlightPlanService
    {

        private readonly Dictionary<string, FlightPlan> _flightPlans;

        public FlightPlanService()
        {
            _flightPlans = new Dictionary<string, FlightPlan>();
        }

        public FlightPlan AddFlightPlan(FlightPlan item)
        {


            if (_flightPlans.ContainsKey(item.FlightId))
            {
                //the key is already exist, return an object with id="-1"
                FlightPlan f = new FlightPlan
                {
                    FlightId = "-1"
                };
                return f;
            }
            _flightPlans.Add(item.FlightId, item);
            return item;
        }

        public FlightPlan GetFlightPlanById(string id)
        {

            if (!_flightPlans.TryGetValue(id, out FlightPlan value))
            {
                // the key isn't in the dictionary.
                return null;
            }

            return value;
        }
    }
}
