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
    public class TrainingController : Controller
    {
        public ActionResult Index()
        {
            if (User.Identity.IsAuthenticated && Session["IdUser"] != null)
            {
                var db = new DatabaseConnection(Server.MapPath("~"));

                db.connection.Open();

                var cmd = new SqlCommand("SELECT * FROM Training WHERE IdUser =" + Session["IdUser"], db.connection);

                var trainingList = new List<TrainingHeader>();

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        trainingList.Add(new TrainingHeader
                        {
                            Id = (int)reader["Id"],
                            Name = (string)reader["Name"],
                            Date = (DateTime)reader["Date"]
                        });
                    }

                    reader.Dispose();
                    cmd.Dispose();
                    db.connection.Close();
                }

                return View(trainingList);
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }

        public ActionResult Details(int id)
        {
            var training = new Training();

            return View(training);
        }
    }
}