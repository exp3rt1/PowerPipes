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
    public class ProfileController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && Session["IdUser"] != null)
            {
                var profile = new Profile();

                var db = new DatabaseConnection(Server.MapPath("~"));
                db.connection.Open();

                var user = UserBL.GetUser((int)Session["IdUser"], db);

                profile.UserName = user.UserName;
                profile.Name = user.Name;
                profile.Age = user.Age;
                profile.IdUser = (int)Session["IdUser"];
                /*var meetResultList = MeetBL.GetResults((int)Session["IdUser"], db);
                var trainingList = TrainingBL.GetTrainings((int)Session["IdUser"], db);

                profile.Name = user.Name;
                profile.Age = user.Age;

                var maxMeetSquat = 0;
                var maxMeetBench = 0;
                var maxMeetDeadlift = 0;
                var maxTrainingSquat = 0;
                var maxTrainingSquatUnit = "";
                var maxTrainingBench = 0;
                var maxTrainingBenchUnit = "";
                var maxTrainingDeadlift = 0;
                var maxTrainingDeadliftUnit = "";

                foreach (var meetResult in meetResultList)
                {
                    if (meetResult.Name.Contains("Squat") && meetResult.Weight > maxMeetSquat)
                    {
                        maxMeetSquat = meetResult.Weight;
                    }

                    if (meetResult.Name.Contains("Bench") && meetResult.Weight > maxMeetBench)
                    {
                        maxMeetBench  = meetResult.Weight;
                    }

                    if (meetResult.Name.Contains("Deadlift") && meetResult.Weight > maxMeetDeadlift)
                    {
                        maxMeetDeadlift = meetResult.Weight;
                    }
                }

                foreach (var training in trainingList)
                {
                    foreach(var exercise in TrainingBL.GetExercices(training.Id, db))
                    {
                        if(TrainingBL.GetMovementName(exercise.MovementType, db).Contains("Squat") && exercise.Weight > maxTrainingSquat)
                        {
                            maxTrainingSquat = exercise.Weight;
                            maxTrainingSquatUnit = exercise.Unit;
                        }

                        if (TrainingBL.GetMovementName(exercise.MovementType, db).Contains("Bench") && exercise.Weight > maxTrainingBench)
                        {
                            maxTrainingBench = exercise.Weight;
                            maxTrainingBenchUnit = exercise.Unit;
                        }

                        if (TrainingBL.GetMovementName(exercise.MovementType, db).Contains("Deadlift") && exercise.Weight > maxTrainingDeadlift)
                        {
                            maxTrainingDeadlift = exercise.Weight;
                            maxTrainingDeadliftUnit = exercise.Unit;
                        }
                    }
                }

                profile.MaxCompetitionSquat = maxMeetSquat;
                profile.MaxCompetitionSquatUnit = "kg";
                profile.MaxCompetitionBench = maxMeetBench;
                profile.MaxCompetitionBenchUnit = "kg";
                profile.MaxCompetitionDeadlift = maxMeetDeadlift;
                profile.MaxCompetitionDeadliftUnit = "kg";

                profile.MaxTrainingSquat = maxTrainingSquat;
                profile.MaxTrainingSquatUnit = maxTrainingSquatUnit;
                profile.MaxTrainingBench = maxTrainingBench;
                profile.MaxTrainingBenchUnit = maxTrainingBenchUnit;
                profile.MaxTrainingDeadlift = maxTrainingDeadlift;
                profile.MaxTrainingDeadliftUnit = maxTrainingDeadliftUnit;*/

                db.connection.Close();

                return View(profile);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        [HttpPost]
        public JsonResult Modify(Profile profile)
        {
            Console.WriteLine("Hello World!");

            var db = new DatabaseConnection(Server.MapPath("~"));
            db.connection.Open();

            UserBL.UpdateUser(profile, db);

            db.connection.Close();

            return Json(new { status = "Saved" });
        }
    }
}