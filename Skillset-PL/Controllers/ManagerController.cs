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
        private readonly IReportingStaffExtensions _reportingStaff;
        private readonly ISkillService _skillService;
        private ISkillRatingService _skillRatingService;
        private readonly IEmployeeServices _employeeServices;
        public ManagerController(IReportingStaffExtensions reportingStaff, ISkillService skillService, ISkillRatingService skillRatingService, IEmployeeServices employeeServices)
        {
            _reportingStaff = reportingStaff;
            _skillService = skillService;
            _skillRatingService = skillRatingService;
            _employeeServices = employeeServices;
        }
        // GET: Manager
        public ActionResult Index(int? page)
        {
            if(Session["customercode"].ToString()!=string.Empty)
            {
                var staff = _reportingStaff.GetEmployeeDetails(Session["customercode"].ToString()).ToReportingStaffViewmodel();
                int pageSize = 3;
                int pageNumber = (page ?? 1);
                return View(staff.ToPagedList(pageNumber, pageSize));
            }
            else
            {
             return RedirectToAction("MyProfile");
            }
            
           
        }
          public ActionResult SkillRate(string code, string name, int? page)
        {
            if(code!=null && name!=null)
            {
                ViewBag.Code = code;
                ViewBag.Name = name;
            }
            
            var skill = _reportingStaff.GetSkillRatingsDetails(code).ToSkillRatingsViewmodel();
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(skill.ToPagedList(pageNumber, pageSize));
        }
        public ActionResult MyProfile()
        {
            var EmployeeDtoList = _employeeServices.GetProfile(Session["customercode"].ToString());
            var profile = EmployeeDtoList.EmployeeDTOtoViewModel();
            Session["customerId"] = EmployeeDtoList.Id;
            profile.DesignationId = _employeeServices.GetDesignationName(profile.DesignationId);
            profile.RoleId = _employeeServices.GetRoleName(profile.RoleId);
            return View("MyProfile",profile);
        }

        public ActionResult ManagerRating()
        {
            var EmpId = Convert.ToInt32(Session["customerId"]);
            EmployeeRatingScreenViewModel ratingObj = new EmployeeRatingScreenViewModel();

            ratingObj.RatedSkills = GetRatedSkills(EmpId);
            ratingObj.SkillRatings = EmployeeRatings();
            return View(ratingObj);
        }
        /// <summary>
        /// Retrieve rated skills of employee
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns>IEnumerable<EmployeeRatedSkillsViewModel></returns>
        public IEnumerable<EmployeeRatedSkillsViewModel> GetRatedSkills(int EmpId)
        {
            var RatedSkills = _skillRatingService.GetRatedSkills(EmpId).ToSkillRatedViewmodel();
            return RatedSkills;
        }
        /// </summary>
        /// <returns>IEnumerable<SkillViewModel></returns>
        public IEnumerable<SkillViewModel> EmployeeRatings()
        {
            var skillList = _skillService.GetAllSkills().ToViewModelList();
            return skillList;
        }

    }
}
