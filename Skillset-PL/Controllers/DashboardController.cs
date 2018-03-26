using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            ViewBag.SkillnameList = "'Java','C','C#','Python'";
            ViewBag.RatingList = "0,90,20,100";
            return View();
        }
    }
}