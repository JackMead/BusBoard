using System.Web.Mvc;
using BusBoard.Web.Models;
using BusBoard.Web.ViewModels;
using BusBoard.Api;
using System.Linq;
using System.Collections.Generic;

namespace BusBoard.Web.Controllers
{
    public class HomeController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult BusInfo(PostcodeSelection selection)
        {

            var busFinder = new BusFinder();
            var postCodeInfo = busFinder.GetPostCodeInformation(selection.Postcode);

            var StopCode="";
            var AllArrivals= new List<ArrivalInformation>();
            if (postCodeInfo.result != null)
            {
                var allStopPoints = busFinder.GetStopPointsFromPostCodeInfo(postCodeInfo.result).StopPoints;
                var listOfStopPointsByDistance = allStopPoints.OrderBy(s => s.Distance).ToList();
                if (listOfStopPointsByDistance.Count != 0)
                {
                    StopCode = listOfStopPointsByDistance[0].NaptanId;
                    AllArrivals = busFinder.ReturnAllArrivalsForStopId(StopCode);
                }
            }

            var info = new BusInfo(selection.Postcode, StopCode, AllArrivals);
            return View(info);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Information about this site";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact us!";

            return View();
        }
    }
}