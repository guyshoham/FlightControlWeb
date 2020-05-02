using FlightControlWeb.Models;
using System;
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
            var rand = new Random();
            string id = (Utils.LongRandom(100000, 9999999999, rand)).ToString();

            while (_flightPlans.ContainsKey(id))
            {
                id = (Utils.LongRandom(100000, 9999999999, rand)).ToString();
            }

            item.FlightPlanId = id;
            _flightPlans.Add(item.FlightPlanId, item);
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
