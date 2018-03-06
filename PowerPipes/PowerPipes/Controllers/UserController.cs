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
            FormsAuthentication.SignOut();
            return View();
        }

        [HttpPost]
        public ActionResult Login(User user)
        {
            if (ModelState.IsValid)
            {
                var db = new DatabaseConnection(Server.MapPath("~"));
                
                db.connection.Open();

                var cmd = new SqlCommand("SELECT Id, Username FROM Users WHERE Username = '"+user.UserName+"' AND Password='"+user.Password+"'", db.connection);

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        Session["IdUser"] = reader["Id"];
                    }

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    reader.Dispose();
                    cmd.Dispose();
                    db.connection.Close();
                    return RedirectToAction("Index", "Training");
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

                    var cmd = new SqlCommand("Insert Into Users (Username,Password) output INSERTED.ID Values('" + user.UserName + "','" + user.Password + "')", db.connection);

                    Session["IdUser"] = (int)cmd.ExecuteScalar();
                    cmd.Dispose();
                    db.connection.Close();

                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    return RedirectToAction("Index", "Training");
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