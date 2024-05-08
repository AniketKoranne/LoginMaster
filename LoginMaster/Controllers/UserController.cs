using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginMaster.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Index()
        {
            string appName = ConfigurationManager.AppSettings["ApplicationName"];
            if (!string.IsNullOrEmpty(appName))
            {
                ViewBag.ApplicationName = appName;
            }
            else
            {
                ViewBag.ApplicationName = "Default Application Name";
            }
            
            return View();
        }
    }
}