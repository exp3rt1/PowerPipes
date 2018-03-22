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