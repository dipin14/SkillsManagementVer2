using PagedList;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    [Authorize(Roles = "Manager")]
    public class ManagerController : Controller
    {
        private readonly IReportingStaffService _reportingStaff;
        private readonly ISkillService _skillService;
        private ISkillRatingService _skillRatingService;
        private readonly IEmployeeServices _employeeServices;
        public ManagerController(IReportingStaffService reportingStaff, ISkillService skillService, ISkillRatingService skillRatingService, IEmployeeServices employeeServices)
        {
            _reportingStaff = reportingStaff;
            _skillService = skillService;
            _skillRatingService = skillRatingService;
            _employeeServices = employeeServices;
        }

        /// <summary>
        /// Redirect to view of staff details.
        /// </summary>
        /// <param name="page"></param>
        /// <returns>Staff Details View</returns>
        public ActionResult Index(int? page)
        {
            if (Session["customercode"] != null)
            {
                var staff = _reportingStaff.GetEmployeeDetails(Session["customercode"].ToString()).ToReportingStaffViewmodel();
                int pageSize = 8;
                int pageNumber = (page ?? 1);
                return View(staff.ToPagedList(pageNumber, pageSize));
            }
            else
            {
                return RedirectToAction("Login", "Login");
            }


        }

        /// <summary>
        /// Redirect to a view of skill ratings of an employee.
        /// </summary>
        /// <param name="code"></param>
        /// <param name="name"></param>
        /// <param name="page"></param>
        /// <returns>View of rated skills</returns>
        public ActionResult SkillRate(string code, string name, int? page)
        {
            if (code == null)
            {
                return RedirectToAction("Login", "Login");
            }
            if (code != null && name != null)
            {
                ViewBag.Code = code;
                ViewBag.Name = name;
            }

            var skill = _reportingStaff.GetSkillRatingsDetails(code).ToSkillRatingsViewmodel();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(skill.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        /// Returns a view of logged in manager's profile.
        /// </summary>
        /// <returns>View of logged in user's profile</returns>
        public ActionResult MyProfile()
        {
            if (Session["customercode"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var EmployeeDtoList = _employeeServices.GetProfile(Session["customercode"].ToString());
            var profile = EmployeeDtoList.EmployeeDTOtoViewModel();
            Session["customerId"] = EmployeeDtoList.Id;
            profile.DesignationId = _employeeServices.GetDesignationName(profile.DesignationId);
            profile.RoleId = _employeeServices.GetRoleName(profile.RoleId);
            return View("MyProfile", profile);
        }

        /// <summary>
        ///Give view a to see rated skills and to rate skills for the logged in manager.
        /// </summary>
        /// <returns>View to rate skills</returns>
        public ActionResult ManagerRating()
        {
            if (Session["customerId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var EmpId = Convert.ToInt32(Session["customerId"]);
            EmployeeRatingScreenViewModel ratingObj = new EmployeeRatingScreenViewModel();
            var ratedList = GetRatedSkills(EmpId);
            var SkillList = EmployeeRatings();
            //var result = ratedList.Where(x => !SkillList.Contains(x.SkillId));
            var result = SkillList.Where(p => !ratedList.Any(p2 => p2.SkillId == p.skillId));
            ratingObj.RatedSkills = ratedList;
            ratingObj.SkillRatings = result;
            ViewBag.IsSpecial = ratingObj.RatedSkills.ToList().Any(m => m.SkillName == "Special skill");
            return View(ratingObj);
        }
        /// <summary>
        /// Retrieve rated skills details of an employee from database.
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns>IEnumerable<EmployeeRatedSkillsViewModel></returns>
        public IEnumerable<EmployeeRatedSkillsViewModel> GetRatedSkills(int EmpId)
        {
            var RatedSkills = _skillRatingService.GetRatedSkills(EmpId).ToSkillRatedViewmodel();
            return RatedSkills;
        }

        /// <summary>
        /// Retrieve all skills
        /// </summary>
        /// <returns>IEnumerable<SkillViewModel></returns>
        public IEnumerable<SkillViewModel> EmployeeRatings()
        {
            var skillList = _skillService.GetAllSkills().ToViewModelList();
            return skillList;
        }
    }
}
