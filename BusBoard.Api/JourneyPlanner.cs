using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusBoard.Api
{
    public class JourneyPlanner
    {
        public int Duration { get; set; }
        public Dictionary<string, int> Steps { get; set; }

        public void PlanFromPostCodes(string origin, string destination)
        {
            var client = new RestClient("https://api.tfl.gov.uk/journey/journeyresults");
            var request = new RestRequest(origin + "/to/"+destination, Method.GET);
            var jsonString = client.Execute(request).Content;

            jsonString = @"{foo:'bar'}";
            var obj = JsonConvert.DeserializeObject(jsonString);
            //var url = (string)obj["journeys"];
            //Duration = GetInformationFromJson(jsonString, "")

        }


        public string GetInformationFromJson(string rawText, string token)
        {
            var obj = JObject.Parse(rawText);
            var result = (string)obj.SelectToken(token);
            return result;
        }
    }

}
