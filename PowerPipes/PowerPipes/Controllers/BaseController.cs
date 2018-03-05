using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PowerPipes.Controllers
{
    public class BaseController : Controller
    {
        public JsonResult Redirect(string actionName, string controllerName, object routeValues)
        {
            return Json(new { redirectUrl = Url.Action(actionName, controllerName, routeValues), isRedirect = true });
        }
    }
}