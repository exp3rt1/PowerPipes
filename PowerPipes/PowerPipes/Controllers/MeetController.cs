using PowerPipes.BL;
using PowerPipes.DataAccess;
using PowerPipes.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PowerPipes.Controllers
{
    public class MeetController : BaseController
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && Session["IdUser"] != null)
            {
				var db = new DatabaseConnection(Server.MapPath("~"));
				db.connection.Open();

				var meetList = MeetBL.GetTrainings((int)Session["IdUser"], db);

				db.connection.Close();

				return View(meetList);
			}
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

		public ActionResult Details(int idMeet)
		{
			var meet = new Meet();
			ViewBag.SuccessChoices = MeetBL.GetSuccessChoices();

			var db = new DatabaseConnection(Server.MapPath("~"));
			db.connection.Open();

			meet.Header = MeetBL.GetHeader(idMeet, db);

			if (meet.Header.IdUser == (int)Session["IdUser"])
			{
				meet.Results = MeetBL.GetResults(idMeet, db);
			}
			//Prevents users from accessing other users trainings
			else
			{
				return RedirectToAction("Index", "Meet");
			}

			db.connection.Close();

			return View(meet);
		}

		public ActionResult Create()
		{
			var meet = new Meet();
			meet.CreateEmptyResults();
			meet.Header.Date = DateTime.Today;

			ViewBag.SuccessChoices = MeetBL.GetSuccessChoices();

			return View(meet);
		}

		[HttpPost]
		public ActionResult Create(Meet meet)
		{
			meet.Header.IdUser = (int)Session["IdUser"];

			var db = new DatabaseConnection(Server.MapPath("~"));
			db.connection.Open();

			MeetBL.CreateMeet(meet, db);

			db.connection.Close();
			return Redirect("Index", "Meet", null);
		}

		[HttpPost]
		public JsonResult Modify(Meet meet)
		{
			return Json(new { status = "Saved" });
		}

		public ActionResult Delete(int idMeet)
		{
			return Redirect("Index", "Meet", null);
		}
	}
}