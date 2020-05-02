﻿using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            if (_flightPlans.ContainsKey(item.flight_id))
            {
                //the key is already exist, return an object with id="-1"
                FlightPlan f = new FlightPlan
                {
                    flight_id = "-1"
                };
                return f;
            }
            _flightPlans.Add(item.flight_id, item);
            return item;
        }

        public FlightPlan GetFlightPlanById(string id)
        {
            FlightPlan value;

            if (!_flightPlans.TryGetValue(id, out value))
            {
                // the key isn't in the dictionary.
                return null;
            }

            return value;
        }
    }
}
