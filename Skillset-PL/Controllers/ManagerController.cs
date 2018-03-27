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
        private readonly IEmployeeServices _employeeServices;
        public ManagerController(IReportingStaffExtensions reportingStaff, IEmployeeServices employeeServices)
        {
            _reportingStaff = reportingStaff;
            _employeeServices = employeeServices;
        }
        
        
        // GET: Manager
        public ActionResult Index()
        {
            //Session["empcode"] = 1;
            var staff = _reportingStaff.GetEmployeeDetails(Session["customercode"].ToString()).ToReportingStaffViewmodel();
            return View(staff);
        }
        
        
        public ActionResult SkillRate(string code, string name)
        {
            ViewBag.Code = code;
            ViewBag.Name = name;
            var skill = _reportingStaff.GetSkillRatingsDetails(code).ToSkillRatingsViewmodel();
            return View(skill);
        }
        public ActionResult MyProfile()
        {
            var profile = _reportingStaff.GetProfile(Session["customercode"].ToString()).EmployeeDTOtoViewModel();
            profile.DesignationId = _employeeServices.GetDesignationName(profile.DesignationId);
            profile.RoleId = _employeeServices.GetRoleName(profile.RoleId);
            return View("MyProfile",profile);
        }
    }
}