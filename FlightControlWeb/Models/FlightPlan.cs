using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlan
    {
        public string flight_id { get; set; }

        public int passengers { get; set; }

        public string company_name { get; set; }

        public InitialLocation initial_location { get; set; }

        public Segment[] segments { get; set; }
    }
}
