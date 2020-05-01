using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FlightControlWeb.Models
{
    public class FlightPlanItems
    {
        public string flight_id { get; set; }

        public int passengers { get; set; }

        public string company_name { get; set; }

        public InitialLocationItems initialLocation { get; set; }

        public SegmentItems[] segmentItems { get; set; }
    }
}
