using FlightControlWeb.Models;

namespace FlightControlWeb.Services
{
    public interface IFlightPlanService
    {
        FlightPlan AddFlightPlan(FlightPlan item);
        FlightPlan GetFlightPlanById(string id);
    }
}
