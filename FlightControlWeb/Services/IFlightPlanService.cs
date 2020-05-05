using FlightControlWeb.Models;
using System;
using System.Collections.Generic;

namespace FlightControlWeb.Services
{
    public interface IFlightPlanService
    {
        FlightPlan AddFlightPlan(FlightPlan item);
        FlightPlan GetFlightPlanById(string id);
        Dictionary<string, FlightPlan> GetAllFlightPlans();
        List<Flight> GetAllFlightsRelativeToDate(Dictionary<string, FlightPlan> plans, DateTime dateInput);
    }
}
