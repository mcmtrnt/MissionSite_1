/*
 * Authors: Joshua Sperry, Trent McMillian, Ian Keller, Samuel Faber
 * Section: IS 403 section 1
 * Description: This is a missions page that helps those getting ready to go out into the mission field.
 */

using MissionSite.DAL;
using MissionSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MissionSite.Controllers
{
    public class HomeController : Controller
    {
        private MissionSiteContext db = new MissionSiteContext();
        private static string currentMissionName;

        public ActionResult Index()
        {
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
            var mission =
                    db.Database.SqlQuery<Mission>(
                "Select * " +
                "FROM Mission " +
                "WHERE MissionName = '" + oMission.MissionName + "'");

            currentMissionName = mission.First().MissionName;

            ViewBag.Mission = db.Missions.Find(mission.First().MissionID);

                return View("MissionView", db.MissionQuestions.ToList());
                 
        }

        public ActionResult MissionView()
        {
            var mission =
                    db.Database.SqlQuery<Mission>(
                "Select * " +
                "FROM Mission " +
                "WHERE MissionName = '" + currentMissionName + "'");

            ViewBag.Mission = db.Missions.Find(mission.First().MissionID);

            return View(db.MissionQuestions.ToList());
        }
            

        [HttpGet]
        public ActionResult Reply(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var mission =
                    db.Database.SqlQuery<Mission>(
                "Select * " +
                "FROM Mission " +
                "WHERE MissionName = '" + currentMissionName + "'");

            MissionQuestion missionQuestion = db.MissionQuestions.Find(id);

            missionQuestion.MissionID = mission.First().MissionID;

            //Need to assign UserID here once login has been set up

            if (missionQuestion == null)
            {
                return HttpNotFound();
            }

            return View(missionQuestion);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Reply([Bind(Include = "MissionQuestionID,MissionID,UserID,Question,Answer")] MissionQuestion missionQuestion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(missionQuestion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("MissionView");
            }

            return View(missionQuestion);
        }

        [HttpGet]
        public ActionResult PostQuestion()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PostQuestion([Bind(Include = "MissionQuestionID,MissionID,UserID,Question,Answer")] MissionQuestion missionQuestion)
        {
            if (ModelState.IsValid)
            {
                var mission =
                    db.Database.SqlQuery<Mission>(
                "Select * " +
                "FROM Mission " +
                "WHERE MissionName = '" + currentMissionName + "'");

                missionQuestion.MissionID = mission.First().MissionID;

                db.MissionQuestions.Add(missionQuestion);
                db.SaveChanges();
                return RedirectToAction("MissionView");
            }

            return View(missionQuestion);
        }

    }
}