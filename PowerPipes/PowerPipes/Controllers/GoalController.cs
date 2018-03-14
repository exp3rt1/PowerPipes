using PowerPipes.BL;
using PowerPipes.DataAccess;
using PowerPipes.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PowerPipes.Controllers
{
    public class GoalController : BaseController
    {
		public ActionResult Index()
		{
			if (User.Identity.IsAuthenticated && Session["IdUser"] != null)
			{
				var db = new DatabaseConnection(Server.MapPath("~"));
				db.connection.Open();

				var goalList = GoalBL.GetGoals((int)Session["IdUser"], db);

				db.connection.Close();

				return View(goalList);
			}
			else
			{
				return RedirectToAction("Login", "User");
			}
		}

		public ActionResult Details(int idGoal)
		{
			ViewBag.Units = GoalBL.GetUnits();

			var db = new DatabaseConnection(Server.MapPath("~"));
			db.connection.Open();

			ViewBag.Movements = GoalBL.GetMovementTypes(db);
			var goal = GoalBL.GetGoal(idGoal, db);

			db.connection.Close();


			if (goal.IdUser == (int)Session["IdUser"])
			{
				return View(goal);
			}
			//Prevents users from accessing other users goals
			else
			{
				return RedirectToAction("Index", "Goal");
			}
		}

		public ActionResult Create()
		{
			var goal = new Goal();
            goal.Date = DateTime.Today;

			ViewBag.Units = GoalBL.GetUnits();

			var db = new DatabaseConnection(Server.MapPath("~"));
			db.connection.Open();

			ViewBag.Movements = GoalBL.GetMovementTypes(db);

			db.connection.Close();

			return View(goal);
		}

		[HttpPost]
		public ActionResult Create(Goal goal)
		{
			goal.IdUser = (int)Session["IdUser"];

			var db = new DatabaseConnection(Server.MapPath("~"));
			db.connection.Open();

			GoalBL.CreateGoal(goal, db);

			db.connection.Close();
			return Redirect("Index", "Goal", null);
		}

		[HttpPost]
		public JsonResult Modify(Goal goal)
		{
			var db = new DatabaseConnection(Server.MapPath("~"));
			db.connection.Open();

			GoalBL.UpdateGoal(goal, db);

			db.connection.Close();

			return Json(new { status = "Saved" });
		}

		public ActionResult Delete(int idGoal)
		{
			var db = new DatabaseConnection(Server.MapPath("~"));
			db.connection.Open();

			GoalBL.DeleteGoal(idGoal, db);

			db.connection.Close();

			return Redirect("Index", "Goal", null);
		}
	}
}