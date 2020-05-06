using FlightControlWeb.Models;
using System;
using System.Collections.Generic;

namespace FlightControlWeb.Services
{
    public interface IFlightService
    {
        Flight AddFlight(Flight item);
        Dictionary<string, Flight> GetFlights();
        FlightPlan DeleteFlightPlanById(string id);
        FlightPlan AddFlightPlan(FlightPlan item);
        FlightPlan GetFlightPlanById(string id);
        List<Flight> GetAllFlightsRelativeToDate(DateTime dateInput);
    }
}
