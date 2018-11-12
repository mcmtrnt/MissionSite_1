/*
 * Authors: Joshua Sperry, Trent McMillian, Ian Keller, Samuel Faber
 * Section: IS 403 section 1
 * Description: This is a missions page that helps those getting ready to go out into the mission field.
 */

using MissionSite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MissionSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            //testing
            //testing
            return View();
        }

        [HttpGet]
        public ActionResult Mission()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Mission(Mission oMission)
        {
            if(oMission.sMission == "Provo South")
            {
                ViewBag.MissionName = "Provo South Mission";
                ViewBag.PresidentName = "Greg Anderson";
                ViewBag.MissionAddress = "TNRB 240";
                ViewBag.Language = "C#";
                ViewBag.DominantReligion = "Accounting";
                ViewBag.Climate = "Hot summers, cold winters, and an occasional Ganderson Gale";
                ViewBag.Symbol = "/Content/provo south.png";
                ViewBag.Background = "CityCenter.jpg";

                return View("MissionView");
            }
            else if (oMission.sMission == "Provo North")
            {
                ViewBag.MissionName = "Provo North Mission";
                ViewBag.PresidentName = "Ernie";
                ViewBag.MissionAddress = "TNRB 240";
                ViewBag.Language = "English";
                ViewBag.Climate = "As many as there are";
                ViewBag.DominantReligion = "Project Management";
                ViewBag.Symbol = "/Content/provo north.png";
                ViewBag.Background = "ProvoTemp2.jpg";

                return View("MissionView");
            }
            else if (oMission.sMission == "Idaho")
            {
                ViewBag.MissionName = "Idaho Mission";
                ViewBag.PresidentName = "President Bill Smith";
                ViewBag.MissionAddress = "Middle of Nowhere";
                ViewBag.Language = "Red Neck";
                ViewBag.Climate = "Cloudy with a chance of Potatoes";
                ViewBag.DominantReligion = "Truck Lovers";
                ViewBag.Symbol = "/Content/Idaho2.jpg";
                ViewBag.Background = "IdahoTemp.jpg";

                return View("MissionView");
            }
            else
            {
                return View();
            }
           
        }
    }
}