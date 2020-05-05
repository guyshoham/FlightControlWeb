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
            string id = Utils.IDGenerator();

            while (_flightPlans.ContainsKey(id))
            {
                id = Utils.IDGenerator();
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
        public Dictionary<string, FlightPlan> GetAllFlightPlans()
        {
            return _flightPlans;
        }
        public List<Flight> GetAllFlightsRelativeToDate(Dictionary<string, FlightPlan> plans, DateTime dateInput)
        {
            List<Flight> list = new List<Flight>();

            // iterate over all plans in dictionary
            foreach (FlightPlan plan in plans.Values)
            {
                // get flight from this plan (if not fit, returns null)
                Flight f = GetFlight(plan, dateInput);
                if (f != null)
                {
                    list.Add(f);
                }
            }

            return list;
        }
        public static Flight GetFlight(FlightPlan plan, DateTime dateInput)
        {
            DateTime departureTime = plan.InitialLocation.DateTime;
            if (DateTime.Compare(departureTime, dateInput) > 0)
            {
                // dateInput is before the flight departure, skip this flight
                return null;
            }

            // check segments one by one until reach time (or not)
            DateTime currTime = departureTime;
            Segment prevSegment = new Segment
            {
                Longitude = plan.InitialLocation.Longitude,
                Latitude = plan.InitialLocation.Latitude
            };

            // iterate over all segments in plan
            foreach (Segment s in plan.Segments)
            {
                if (DateTime.Compare(currTime.AddSeconds(s.TimespanSeconds), dateInput) > 0)
                {
                    TimeSpan diff = dateInput - currTime;

                    // we need to calculate location in this segment
                    Segment retVal = Utils.GetLocation(prevSegment, s, diff.TotalSeconds);

                    // create Flight object
                    Flight flight = new Flight
                    {
                        FlightId = plan.FlightPlanId,
                        Longitude = retVal.Longitude,
                        Latitude = retVal.Latitude,
                        Passengers = plan.Passengers,
                        CompanyName = plan.CompanyName,
                        DateTime = dateInput,
                        IsExternal = false // TODO: what value does it get and from where?
                    };
                    return flight;
                }
                currTime = currTime.AddSeconds(s.TimespanSeconds);
                prevSegment = s;
            }

            // dateInput is after the flight landing, skip this flight
            return null;
        }
    }
}
