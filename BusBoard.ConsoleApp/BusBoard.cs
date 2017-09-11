using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class BusBoard
    {
        public void Setup()
        {
            promptUserForPostcode();
            var stopID = "490008660N";
            DisplayNextFiveArrivalsForStopId(stopID);

            var rawPostCodeContent = GetPostCodeInformation("NW1 0TL");
            var postCodeInfo = JsonConvert.DeserializeObject<PostCodeResponse>(rawPostCodeContent).result;
            Console.WriteLine(postCodeInfo.longitude);
            Console.WriteLine(postCodeInfo.latitude);
            

            var stopPoints= GetStopPointsFromPostCodeInfo(postCodeInfo);
            DisplayNextFiveArrivalsForStopId(stopPoints.StopPoints[0].NaptanId);
        }

        private void promptUserForPostcode()
        {
            
        }

        private void DisplayNextFiveArrivalsForStopId(string stopID)
        {
            var rawTFLContent = GetArrivalInformation(stopID);

            var allArrivals = JsonConvert.DeserializeObject<List<ArrivalInformation>>(rawTFLContent);

            foreach (var arrival in allArrivals.OrderBy(a => a.timeToStation).Take(5))
            {
                Console.WriteLine(arrival.lineName + " " + arrival.timeToStation);
            }
        }

        private StopPointsResponseWrapper GetStopPointsFromPostCodeInfo(PostCodeInformation postcode)
        {
            var client = "https://api.tfl.gov.uk";
            var request = "StopPoint?stopTypes=NaptanPublicBusCoachTram&lon=" + postcode.longitude + "&lat=" + postcode.latitude + "&radius=300";
            return GetAPIResultStopPointWrapper(client, request);
        }

        private string GetArrivalInformation(string stopID)
        {
            var client = "https://api-radon.tfl.gov.uk";
            var arrivalRequest = "StopPoint/" + stopID + "/Arrivals";

            return GetAPIResult(client, arrivalRequest);
        }

        private string GetPostCodeInformation(string postCode)
        {
            return GetAPIResult("https://api.postcodes.io", "postcodes/" + postCode);
        }

        private string GetAPIResult(string url, string requestString)
        {
            var client = new RestClient(url);
            var request = new RestRequest(requestString, Method.GET);

            IRestResponse response = client.Execute(request);
            return response.Content;

        }

        private StopPointsResponseWrapper GetAPIResultStopPointWrapper(string url, string requestString)
        {
            var client = new RestClient(url);
            var request = new RestRequest(requestString, Method.GET);

            return client.Execute<StopPointsResponseWrapper>(request).Data;
            

        }
    }
}
