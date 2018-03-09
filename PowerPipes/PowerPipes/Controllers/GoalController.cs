using PowerPipes.Models;
using System;
using System.Collections.Generic;
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
                return View();
            }
            else
            {
                return RedirectToAction("Login", "User");
            }
        }
    }
}