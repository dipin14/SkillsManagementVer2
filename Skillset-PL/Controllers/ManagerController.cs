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
        public ManagerController(IReportingStaffExtensions reportingStaff, ISkillService skillService, ISkillRatingService skillRatingService)
        {
            _reportingStaff = reportingStaff;
            _skillService = skillService;
            _skillRatingService = skillRatingService;
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
        public IEnumerable<EmployeeRatedSkillsViewModel> GetRatedSkills(int EmpId)
        {
            var RatedSkills = _skillRatingService.GetRatedSkills(EmpId).ToSkillRatedViewmodel();
            return RatedSkills;
        }
        public IEnumerable<SkillViewModel> EmployeeRatings()
        {

            var skillList = _skillService.GetAllSkills().ToViewModelList();

            return skillList;

        }
    }
}