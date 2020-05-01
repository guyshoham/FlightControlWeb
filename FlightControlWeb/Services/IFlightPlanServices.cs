using FlightControlWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Services
{
    public interface IFlightPlanServices
    {
        FlightPlanItems AddFlightPlanItems(FlightPlanItems items);
        Dictionary<string, FlightPlanItems> GetFlightPlanItems();
    }
}
