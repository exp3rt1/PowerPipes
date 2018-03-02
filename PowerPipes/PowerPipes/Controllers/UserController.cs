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

                string _sql = @"SELECT [Username] FROM [dbo].[System_Users] " +
                    @"WHERE [Username] = @u AND [Password] = @p";
                var cmd = new SqlCommand(_sql, db.connection);
                cmd.Parameters
                    .Add(new SqlParameter("@u", SqlDbType.NVarChar))
                    .Value = user.UserName;
                cmd.Parameters
                    .Add(new SqlParameter("@p", SqlDbType.NVarChar))
                    .Value = user.Password;

                var reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, true);
                    db.connection.Close();
                    return RedirectToAction("Index", "Planning");
                }
                else
                {
                    reader.Dispose();
                    cmd.Dispose();
                    db.connection.Close();
                    ModelState.AddModelError("", "t poche");
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