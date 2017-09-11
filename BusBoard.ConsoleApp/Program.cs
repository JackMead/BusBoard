using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            var stopID = "490008660N";
            var rawTFLContent = GetArrivalInformation(stopID);

            var allArrivals = JsonConvert.DeserializeObject<List<ArrivalInformation>>(rawTFLContent);

            foreach(var arrival in allArrivals.OrderBy(a => a.timeToStation).Take(5))
            {
                Console.WriteLine(arrival.lineName + " " + arrival.timeToStation);
            }

            var rawPostCodeContent = GetPostCodeInformation("NW1 0TL");
            var postCodeInfo = JsonConvert.DeserializeObject<PostCodeResponse>(rawPostCodeContent).result;
            Console.WriteLine(postCodeInfo.longitude);
            Console.WriteLine(postCodeInfo.latitude);

            Console.WriteLine(GetStopPointsFromPostCodeInfo(postCodeInfo));
            
        }

        private static string GetStopPointsFromPostCodeInfo(PostCodeInformation postcode)
        {
            var client = "https://api.tfl.lu";
            var request = "v1/StopPoint/around/"+ postcode.longitude.ToString() + "/" +
                postcode.latitude.ToString() + "/100000";
            return GetAPIResult(client, request);
        }

        private static string GetArrivalInformation(string stopID)
        {
            var client ="https://api-radon.tfl.gov.uk";
            var arrivalRequest = "StopPoint/" + stopID + "/Arrivals";
           
            return GetAPIResult(client,arrivalRequest);
        }

        private static string GetPostCodeInformation(string postCode)
        {
            return GetAPIResult("https://api.postcodes.io", "postcodes/"+postCode);
        }

        private static string GetAPIResult(string url, string requestString)
        {
            var client = new RestClient(url);
            var request = new RestRequest(requestString, Method.GET);
            
            IRestResponse response = client.Execute(request);
            return response.Content;

        }
    }
}
