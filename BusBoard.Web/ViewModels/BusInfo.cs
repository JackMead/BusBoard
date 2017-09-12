using BusBoard.Api;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public BusInfo(string PostCode, string StopCodeName, List<ArrivalInformation> AllArrivals,
            decimal Latitude, decimal Longitude, JourneyPlanner Planner)
        {
            this.PostCode = PostCode;
            this.StopCodeName = StopCodeName;
            this.AllArrivals = AllArrivals;
            this.Latitude = Latitude;
            this.Longitude = Longitude;
            this.Planner = Planner;
        }

        public string PostCode { get; set; }
        public JourneyPlanner Planner { get; set; }

        public string StopCodeName { get; set; }

        public List<ArrivalInformation> AllArrivals { get; set; }

        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
    }
}