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
                ViewBag.MissionName = "Provo South";
                ViewBag.PresidentName = "Greg Anderson";
                ViewBag.MissionAddress = "TNRB 240";
                ViewBag.Language = "C#";
                ViewBag.DominantReligion = "Accounting";
                ViewBag.Symbol = "/Content/provo south.png";

                return View("ProvoSouth");
            }
            else if (oMission.sMission == "Provo North")
            {
                ViewBag.MissionName = "Provo North";
                ViewBag.PresidentName = "Ernie";
                ViewBag.MissionAddress = "TNRB 240";
                ViewBag.Language = "English";
                ViewBag.DominantReligion = "Project Management";
                ViewBag.Symbol = "/Content/provo north.png";

                return View("ProvoNorth");
            }
            else if (oMission.sMission == "Idaho")
            {
                ViewBag.MissionName = "Idaho";
                ViewBag.PresidentName = "President Bill Smith";
                ViewBag.MissionAddress = "Middle of Nowhere";
                ViewBag.Language = "Red Neck";
                ViewBag.DominantReligion = "Truck Lovers";
                ViewBag.Symbol = "/Content/Idaho2.jpg";

                return View("Idaho");
            }
            else
            {
                return View();
            }
           
        }
    }
}