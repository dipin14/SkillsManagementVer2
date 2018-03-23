using Skillset_PL.ViewModelExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    public class ManagerController : Controller
    {

        ManagerExtension view = new ManagerExtension(); 
        // GET: Manager
        public ActionResult Index()
        {
            Session["empcode"] = 34;

            var staff = view.GetEmployeeDetails(Session["empcode"].ToString());
            return View(staff);
        }
    }
}