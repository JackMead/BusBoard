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
            var postCode = PromptUserForPostcode();

            var rawPostCodeContent = GetPostCodeInformation(postCode);
            var postCodeInfo = JsonConvert.DeserializeObject<PostCodeResponse>(rawPostCodeContent).result;
            
            var stopPoints= GetStopPointsFromPostCodeInfo(postCodeInfo);
            //TODO
            //stopPoints = (StopPointsResponseWrapper) stopPoints.StopPoints.OrderBy(s => s.Distance);
            DisplayNextFiveArrivalsForStopId(stopPoints.StopPoints[0].NaptanId);
        }

        private string PromptUserForPostcode()
        {
            while (true)
            {
                Console.WriteLine("What postcode would you like to find buses for?");
                var userPostCode = Console.ReadLine();
                if (isValidPostcode(userPostCode))
                {
                    return userPostCode;
                }
                Console.WriteLine("Sorry, I didn't understand that");
            }
        }

        private bool isValidPostcode(string userPostCode)
        {
            return true;
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
