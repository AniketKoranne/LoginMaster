using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LoginMaster.Controllers
{
    public class HomeController : Controller
    {
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

        public ActionResult About()
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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
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
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}