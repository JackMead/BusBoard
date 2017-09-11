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

            var request = new RestRequest("StopPoint/490008660N/Arrivals", Method.GET);
            
            // execute the request
            IRestResponse response = client.Execute(request);
            var content = response.Content; // raw content as string
            Console.WriteLine(content);

        }
    }
}
