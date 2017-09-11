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

            var postCodeResponse = GetPostCodeInformation(postCode);

            var stopPoints = GetStopPointsFromPostCodeInfo(postCodeResponse.result);
            var listOfStopPointsByDistance = stopPoints.StopPoints.OrderBy(s => s.Distance).ToList();
            DisplayNextFiveArrivalsForStopId(listOfStopPointsByDistance[0].NaptanId);

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
            if (userPostCode == "")
            {
                return false;
            }
            return true;
        }

        private void DisplayNextFiveArrivalsForStopId(string stopID)
        {
            var allArrivals = GetArrivalInformation(stopID);


            Console.WriteLine("Bus #:\t\t Arriving in:");
            foreach (var arrival in allArrivals.OrderBy(a => a.TimeToStation).Take(5))
            {
                Console.WriteLine(arrival.LineName + "\t\t" + Math.Ceiling((decimal)arrival.TimeToStation / 60) + " mins");
            }
        }

        private StopPointsResponse GetStopPointsFromPostCodeInfo(PostCodeInformation postcode)
        {
            var client = "https://api.tfl.gov.uk";
            var request = "StopPoint?stopTypes=NaptanPublicBusCoachTram&lon=" + postcode.Longitude + "&lat=" + postcode.Latitude + "&radius=300";
            return GetAPIResults<StopPointsResponse>(client, request);
        }

        private List<ArrivalInformation> GetArrivalInformation(string stopID)
        {
            var client = "https://api-radon.tfl.gov.uk";
            var arrivalRequest = "StopPoint/" + stopID + "/Arrivals";

            return GetAPIResults<List<ArrivalInformation>>(client, arrivalRequest);
        }

        private PostCodeResponse GetPostCodeInformation(string postCode)
        {
            string url = "https://api.postcodes.io";
            string requestString = "postcodes/" + postCode;
            return GetAPIResults<PostCodeResponse>(url, requestString);
        }
        
        private T GetAPIResults<T>(string url, string requestString) where T : new()
        {
            var client = new RestClient(url);
            var request = new RestRequest(requestString, Method.GET);
            return client.Execute<T>(request).Data;
        }
    }
}
