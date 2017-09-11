using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BusBoard.Api
{
    public class BusFinder
    {
        public void Setup()
        {
            var postCode = "NW51TL";
            var postCodeResponse = GetPostCodeInformation(postCode);

            var stopPoints = GetStopPointsFromPostCodeInfo(postCodeResponse.result);
            var listOfStopPointsByDistance = stopPoints.StopPoints.OrderBy(s => s.Distance).ToList();
            ReturnAllArrivalsForStopId(listOfStopPointsByDistance[0].NaptanId);

        }

        public List<ArrivalInformation> ReturnAllArrivalsForStopId(string stopID)
        {
            var allArrivals = GetArrivalInformation(stopID);

            return allArrivals.OrderBy(a => a.TimeToStation).ToList();
        }

        public StopPointsResponse GetStopPointsFromPostCodeInfo(PostCodeInformation postcode)
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

        public PostCodeResponse GetPostCodeInformation(string postCode)
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
