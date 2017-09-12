using BusBoard.Api;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public BusInfo(string PostCode, string StopCode, List<ArrivalInformation> AllArrivals)
        {
            this.PostCode = PostCode;
            this.StopCode = StopCode;
            this.AllArrivals = AllArrivals;

        }

        public string PostCode { get; set; }

        public string StopCode { get; set; }

        public List<ArrivalInformation> AllArrivals { get; set; }
    }
}