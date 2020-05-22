using FlightControlWeb.Models;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;


namespace FlightControlWeb.Services
{
    public class FlightService : IFlightService
    {
        private static ConcurrentDictionary<string, FlightPlan> _flightPlans;

        public FlightService()
        {
            _flightPlans = new ConcurrentDictionary<string, FlightPlan>();
        }
        public List<Flight> GetAllFlightsRelativeToDate(DateTime dateInput)
        {
            dateInput = dateInput.ToUniversalTime();
            List<Flight> list = new List<Flight>();

            // iterate over all internal plans in dictionary
            foreach (FlightPlan plan in _flightPlans.Values)
            {
                // get flight from this plan (if not fit, returns null)
                Flight f = GetFlight(plan, dateInput, false);
                if (f != null)
                {
                    list.Add(f);
                }
            }

            return list;
        }
        public FlightPlan AddFlightPlan(FlightPlan item)
        {
            string id = Utils.IDGenerator();

            while (_flightPlans.ContainsKey(id))
            {
                id = Utils.IDGenerator();
            }

            item.FlightId = id;
            _flightPlans.TryAdd(item.FlightId, item);

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
        public FlightPlan DeleteFlightPlanById(string id)
        {
            _flightPlans.TryRemove(id, out FlightPlan value);
            return value;
        }

      

        public static Flight GetFlight(FlightPlan plan, DateTime dateInput, bool isExternal)
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
                        FlightId = plan.FlightId,
                        Longitude = retVal.Longitude,
                        Latitude = retVal.Latitude,
                        Passengers = plan.Passengers,
                        CompanyName = plan.CompanyName,
                        DateTime = dateInput,
                        IsExternal = isExternal
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
