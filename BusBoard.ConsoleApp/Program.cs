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

            var client = new RestClient("https://api-radon.tfl.gov.uk");

            var stopID = "490008660N";
            var arrivalRequest= "StopPoint/" + stopID + "/Arrivals";
            var request = new RestRequest( arrivalRequest, Method.GET);
            
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content;

            var allArrivals = JsonConvert.DeserializeObject<List<ArrivalInformation>>(content);

            foreach(var arrival in allArrivals.OrderBy(a => a.timeToStation).Take(5))
            {
                Console.WriteLine(arrival.lineName + " " + arrival.timeToStation);
            }

            
        }
    }
}
