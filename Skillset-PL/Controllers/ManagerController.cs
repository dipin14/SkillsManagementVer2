using Skillset_BLL.Services;
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

        private readonly IReportingStaffExtensions _reportingStaff;

        public ManagerController(IReportingStaffExtensions reportingStaff)
        {
            this._reportingStaff = reportingStaff;
        }
        
        
        // GET: Manager
        public ActionResult Index()
        {
            Session["empcode"] = 34;

            var staff = _reportingStaff.GetEmployeeDetails(Session["empcode"].ToString()).ToReportingStaffViewmodel();
            return View(staff);
        }
        
        
        public ActionResult SkillRate(string code)
        {
            var skill = _reportingStaff.GetSkillRatingsDetails(code).ToSkillRatingsViewmodel();
            return View(skill);
        }

        public ActionResult MyProfile()
        {
            return View("Profile");
        }
    }
}