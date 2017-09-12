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
        public Dictionary<string, int> JourneySteps { get; set; }

        public JourneyPlanner()
        {
            JourneySteps = new Dictionary<string, int>{ };
        }

        public void PlanFromPostCodes(string origin, string destination)
        {
            var client = new RestClient("https://api.tfl.gov.uk/journey/journeyresults");
            var request = new RestRequest(origin + "/to/"+destination, Method.GET);
            var jsonString = client.Execute(request).Content;
            
            var allJouneys = JObject.Parse(jsonString)["journeys"];

            if(allJouneys!=null && allJouneys.Count() > 0)
            {
                var bestJourney = allJouneys[0];
                Duration = (int)bestJourney["duration"];

                foreach(var step in bestJourney["legs"])
                {
                    var summary = (string)step["instruction"]["summary"];
                    var duration = (int)step["duration"];
                    JourneySteps.Add(summary, duration);
                }
            }

        }


        public string GetInformationFromJson(string rawText, string token)
        {
            var obj = JObject.Parse(rawText);
            var result = (string)obj.SelectToken(token);
            return result;
        }
    }

}
