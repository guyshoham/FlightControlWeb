using FlightControlWeb.Models;
using System;
using System.Collections.Generic;

namespace FlightControlWeb.Services
{
    public interface IFlightService
    {
        List<Flight> GetAllFlightsRelativeToDate(DateTime dateInput);
        FlightPlan AddFlightPlan(FlightPlan item);
        FlightPlan GetFlightPlanById(string id);
        FlightPlan DeleteFlightPlanById(string id);
    }
}
