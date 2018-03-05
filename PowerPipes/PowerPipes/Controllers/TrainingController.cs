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
    public class TrainingController : BaseController
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && Session["IdUser"] != null)
            {
                var db = new DatabaseConnection(Server.MapPath("~"));
                db.connection.Open();

                var trainingList = TrainingBL.GetTrainings((int)Session["IdUser"], db);

                db.connection.Close();
                
                return View(trainingList);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Details(int idTraining)
        {
            ViewBag.Units = TrainingBL.GetUnits();

            var training = new Training();

            var db = new DatabaseConnection(Server.MapPath("~"));
            db.connection.Open();

            ViewBag.Movements = TrainingBL.GetMovementTypes(db);
            training.Header = TrainingBL.GetHeader(idTraining, db);

            if (training.Header.IdUser == (int)Session["IdUser"])
            {
                training.Exercises = TrainingBL.GetExercices(idTraining, db);
            }
            //Prevents users from accessing other users trainings
            else
            {
                return RedirectToAction("Index", "Training");
            }

            db.connection.Close();

            if (!training.Exercises.Any())
            {
                training.Exercises.Add(new Exercise());
            }

            return View(training);
        }

        public ActionResult Create()
        {
            var training = new Training();
            training.Header.Date = DateTime.Today;
            training.Exercises.Add(new Exercise());

            ViewBag.Units = TrainingBL.GetUnits();

            var db = new DatabaseConnection(Server.MapPath("~"));
            db.connection.Open();

            ViewBag.Movements = TrainingBL.GetMovementTypes(db);

            db.connection.Close();

            return View(training);
        }

        [HttpPost]
        public ActionResult Create(Training training)
        {
            training.Header.IdUser = (int)Session["IdUser"];

            var db = new DatabaseConnection(Server.MapPath("~"));
            db.connection.Open();

            TrainingBL.CreateTraining(training, db);

            db.connection.Close();
            return Redirect("Index", "Training", null);
        }

        [HttpPost]
        public JsonResult Modify(Training training)
        {
            var db = new DatabaseConnection(Server.MapPath("~"));
            db.connection.Open();

            TrainingBL.UpdateTraining(training, db);

            db.connection.Close();

            return Json(new { status = "Saved" });
        }

        public ActionResult Delete(int idTraining)
        {
            var db = new DatabaseConnection(Server.MapPath("~"));
            db.connection.Open();

            TrainingBL.DeleteTraining(idTraining, db);

            db.connection.Close();

            return Redirect("Index", "Training", null);
        }
    }
}