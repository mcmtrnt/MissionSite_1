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
using System.Web.Security;

namespace MissionSite.Controllers
{
    public class HomeController : Controller
    {
        private MissionSiteContext db = new MissionSiteContext();
        private static string currentMissionName;
        private static string currentUserName;
        private static string currentPassword;

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

        [Authorize]
        public ActionResult FAQ(Mission oMission)
        {
            var mission =
                    db.Database.SqlQuery<Mission>(
                "Select * " +
                "FROM Mission " +
                "WHERE MissionName = '" + currentMissionName + "'");

            ViewBag.Mission = db.Missions.Find(mission.First().MissionID);

            return View(db.MissionQuestions.ToList());
        }


        public ActionResult MissionView(Mission oMission)
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

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]
        public ActionResult Login(FormCollection form, bool rememberMe = false)
        {
            String username = form["UserName"].ToString();
            String password = form["Password"].ToString();

            var LoginID =
                    db.Database.SqlQuery<User>(
                "Select * " +
                "FROM [User] " +
                "WHERE UserEmail = '" + username + "' AND " +
                "Password = '" + password + "'");

            if (LoginID.Count() > 0)
            {
                //this is how customers log in
                if (string.Equals(username, LoginID.First().UserEmail) && (string.Equals(password, LoginID.First().Password)))
                {
                    FormsAuthentication.SetAuthCookie(username, rememberMe);
                    currentUserName = username;
                    currentPassword = password;

                    ViewBag.Name = LoginID.First().FirstName;
                    ViewBag.CustID = LoginID.First().UserID;

                    return RedirectToAction("Index", "Home"); //cust view
                }

                else
                {
                    return View();
                }

            }

            else
            {
                return View();
            }

        }

        public ActionResult SignUp()
        {
            ViewBag.EmpID = new SelectList(db.Users, "UserID", "UserEmail");
            ViewBag.Employees = db.Users.ToList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult SignUp([Bind(Include = "UserID,UserEmail,Password,FirstName,LastName")] User user)
        {
            if (ModelState.IsValid)
            {

                var currentUser =
                db.Database.SqlQuery<User>(
            "Select * " +
            "FROM [User] " +
            "WHERE UserEmail = '" + user.UserEmail + "' AND " +
            "Password = '" + user.Password + "'");

                if (currentUser.Count() > 0)
                {
                    //FormsAuthentication.SetAuthCookie(username, rememberMe);
                    ViewBag.Message = "That username and password are already being used.";
                    ViewBag.Employees = db.Users.ToList();
                    return View("SignUp");
                    //I should inform them that the username or password is already taken.

                }

                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmpID = new SelectList(db.Users, "EmpID", "EmpName", user.UserID);
            ViewBag.Employees = db.Users.ToList();
            return View("Index");
        }

    }
}