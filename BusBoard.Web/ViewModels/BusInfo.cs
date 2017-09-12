using BusBoard.Api;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public BusInfo(string PostCode, string StopCodeName, List<ArrivalInformation> AllArrivals)
        {
            this.PostCode = PostCode;
            this.StopCodeName = StopCodeName;
            this.AllArrivals = AllArrivals;

        }

        public string PostCode { get; set; }

        public string StopCodeName { get; set; }

        public List<ArrivalInformation> AllArrivals { get; set; }

        public string StopCodeId { get; set; }
    }
}