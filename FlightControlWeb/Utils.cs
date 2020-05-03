using System;
using System.Collections.Generic;
using FlightControlWeb.Models;

namespace FlightControlWeb
{
    public class Utils
    {
        /// <summary>
        /// ID structure: X000-000
        /// X represents letter [A-Z]
        /// 0 represents a digit [0-9]
        /// </summary>
        /// <returns>ID string</returns>
        public static string IDGenerator()
        {
            Random rand = new Random();
            int letNum = rand.Next(0, 26); // [0-25]
            char letter = (char)('A' + letNum); // random letter [A-Z]

            string dig1 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig2 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig3 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig4 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig5 = (rand.Next(0, 10)).ToString(); // random digit [0-9]
            string dig6 = (rand.Next(0, 10)).ToString(); // random digit [0-9]

            return letter + dig1 + dig2 + dig3 + '-' + dig4 + dig5 + dig6;
        }

        public static List<Flight> GetAllFlightsRelativeToDate(Dictionary<string, FlightPlan> plans, DateTime dateInput)
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
                currTime = currTime.AddSeconds(s.TimespanSeconds);
                if (DateTime.Compare(currTime, dateInput) > 0)
                {
                    // we need to calculate location in this segment
                    int time = currTime.Second - dateInput.Second;
                    Segment retVal = GetLocation(prevSegment, s, time);

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
                prevSegment = s;
            }

            // dateInput is after the flight landing, skip this flight
            return null;
        }

        public static Segment GetLocation(Segment pointA, Segment pointB, int duration)
        {
            //get pace of lat/long relative to one second
            double paceLat = (pointB.Latitude - pointA.Latitude) / pointB.TimespanSeconds;
            double paceLong = (pointB.Longitude - pointA.Longitude) / pointB.TimespanSeconds;

            //get lat/long for the disire location
            double latitude = pointA.Latitude + paceLat * duration;
            double longitude = pointA.Longitude + paceLong * duration;

            // create new Segment with the lat/long result
            Segment location = new Segment
            {
                Latitude = latitude,
                Longitude = longitude
            };

            return location;
        }
    }
}
