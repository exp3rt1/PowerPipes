using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PowerPipes.BL;
using PowerPipes.DataAccess;
using PowerPipes.Models;

namespace PowerPipes.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && Session["IdUser"] != null)
            {
                var dashboard = new Dashboard();

                var db = new DatabaseConnection(Server.MapPath("~"));
                db.connection.Open();

                var user = UserBL.GetUser((int)Session["IdUser"], db);
                var meetResultList = MeetBL.GetResultsForUser((int)Session["IdUser"], db);
                var trainingList = TrainingBL.GetTrainings((int)Session["IdUser"], db);

                dashboard.Name = user.Name;

                var maxMeetSquat = 0.0f;
                var maxMeetBench = 0.0f;
                var maxMeetDeadlift = 0.0f;
                var maxTrainingSquat = 0.0f;
                var maxTrainingSquatUnit = "";
                var maxTrainingBench = 0.0f;
                var maxTrainingBenchUnit = "";
                var maxTrainingDeadlift = 0.0f;
                var maxTrainingDeadliftUnit = "";

                foreach (var meetResult in meetResultList)
                {
                    if (meetResult.Success)
                    {
                        if (meetResult.MovementType == 1 && meetResult.Weight > maxMeetSquat)
                        {
                            maxMeetSquat = meetResult.Weight;
                        }

                        if (meetResult.MovementType == 2 && meetResult.Weight > maxMeetBench)
                        {
                            maxMeetBench = meetResult.Weight;
                        }

                        if (meetResult.MovementType == 3 && meetResult.Weight > maxMeetDeadlift)
                        {
                            maxMeetDeadlift = meetResult.Weight;
                        }
                    }
                }

                foreach (var training in trainingList)
                {
                    foreach (var exercise in TrainingBL.GetExercices(training.Id, db))
                    {
                        if (exercise.MovementType == 1 && exercise.Weight > maxTrainingSquat)
                        {
                            maxTrainingSquat = exercise.Weight;
                            maxTrainingSquatUnit = exercise.Unit;
                        }

                        if (exercise.MovementType == 2 && exercise.Weight > maxTrainingBench)
                        {
                            maxTrainingBench = exercise.Weight;
                            maxTrainingBenchUnit = exercise.Unit;
                        }

                        if (exercise.MovementType == 3 && exercise.Weight > maxTrainingDeadlift)
                        {
                            maxTrainingDeadlift = exercise.Weight;
                            maxTrainingDeadliftUnit = exercise.Unit;
                        }
                    }
                }

                dashboard.MaxCompetitionSquat = maxMeetSquat;
                dashboard.MaxCompetitionSquatUnit = "kg";
                dashboard.MaxCompetitionBench = maxMeetBench;
                dashboard.MaxCompetitionBenchUnit = "kg";
                dashboard.MaxCompetitionDeadlift = maxMeetDeadlift;
                dashboard.MaxCompetitionDeadliftUnit = "kg";

                dashboard.MaxTrainingSquat = maxTrainingSquat;
                dashboard.MaxTrainingSquatUnit = maxTrainingSquatUnit;
                dashboard.MaxTrainingBench = maxTrainingBench;
                dashboard.MaxTrainingBenchUnit = maxTrainingBenchUnit;
                dashboard.MaxTrainingDeadlift = maxTrainingDeadlift;
                dashboard.MaxTrainingDeadliftUnit = maxTrainingDeadliftUnit;

                dashboard.SquatProgression = TrainingBL.GetSquatProgression((int)Session["IdUser"], db);

                dashboard.BenchProgression = TrainingBL.GetBenchProgression((int)Session["IdUser"], db);

                dashboard.DeadliftProgression = TrainingBL.GetDeadliftProgression((int)Session["IdUser"], db);

                db.connection.Close();

                return View(dashboard);
            }

            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}