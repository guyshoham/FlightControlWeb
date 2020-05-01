using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public interface IFlightPlanService
    {
        FlightPlan AddFlightPlan(FlightPlan item);
        FlightPlan GetFlightPlanById(string id);
    }
}
