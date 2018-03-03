using PowerPipes.DataAccess;
using PowerPipes.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace PowerPipes.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var db = new DatabaseConnection(Server.MapPath("~"));
                
                db.connection.Open();

                string _sql = @"SELECT [Username] FROM [dbo].[Users] " +
                    @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand("SELECT Username FROM Users WHERE Username = '"+user.UserName+"' AND Password='"+user.Password+"'", db.connection);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    reader.Dispose();
                    cmd.Dispose();
                    db.connection.Close();
                    return RedirectToAction("Index", "Planning");
                }

                reader.Dispose();
                cmd.Dispose();
                db.connection.Close();
                ModelState.AddModelError("", "Combinaison Utilisateur et Mot de passe invalide");
            }
            
            return View(user);
        }

        public ActionResult New()
        {
            return View(new User());
        }

        [HttpPost]
        public ActionResult New(User user)
        {
            if (ModelState.IsValid)
            {
                if (user.Password == user.ConfirmPassword)
                {
                    var db = new DatabaseConnection(Server.MapPath("~"));

                    db.connection.Open();

                    var cmd = new SqlCommand("Insert Into Users (Username,Password) Values('" + user.UserName + "','" + user.Password + "')", db.connection);

                    cmd.ExecuteNonQuery();
                    cmd.Dispose();
                    db.connection.Close();

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    return RedirectToAction("Index", "Planning");
                }
                else
                {
                    ModelState.AddModelError("", "Veuillez valider que les mots de passes sont identiques");
                }
            }

            return View(user);
        }


        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "User");
        }
    }
}