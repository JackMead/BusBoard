using BusBoard.Api;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Web.ViewModels
{
    public class BusInfo
    {
        public BusInfo(BusFinder busFinder, string postCode)
        {
            PostCode = postCode;
            var postCodeInfo = busFinder.GetPostCodeInformation(postCode);

            var allStopPoints = busFinder.GetStopPointsFromPostCodeInfo(postCodeInfo.result).StopPoints;
            var listOfStopPointsByDistance = allStopPoints.OrderBy(s => s.Distance).ToList();
            StopCode = listOfStopPointsByDistance[0].NaptanId;

            AllArrivals = busFinder.ReturnAllArrivalsForStopId(StopCode);
        }

        public string PostCode { get; set; }

        public string StopCode { get; set; }

        public List<ArrivalInformation> AllArrivals { get; set; }
    }
}