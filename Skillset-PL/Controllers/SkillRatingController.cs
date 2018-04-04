using System;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    [Authorize(Roles = "Employee,Manager")]
    public class SkillRatingController : Controller
    {
        private readonly ISkillService _skillService;
        private readonly ISkillRatingService _skillRatingService;
        private readonly IEmployeeServices _employeeServices;

        public SkillRatingController(ISkillService skillService, ISkillRatingService skillRatingService, IEmployeeServices employeeServices)
        {
            _skillService = skillService;
            _skillRatingService = skillRatingService;
            _employeeServices = employeeServices;
        }

        public ActionResult GetAllSkills()
        {
            return View();
        }

        public IEnumerable<SkillViewModel> EmployeeRatings()
        {
            var skillList = _skillService.GetAllSkills().ToViewModelList();

            return skillList;

        }
        public ActionResult RateSkills(List<EmployeeSkillRatingViewModel> ratingList)
        {

            if (ratingList != null)
            {

                ratingList.ForEach(m => { m.EmployeeId = Convert.ToInt32(Session["customerId"]); m.Status = true; m.RatingDate = DateTime.Now; });
                var result = _skillRatingService.Create(ratingList.ToSkillRatingDTOList());

                return View(result);
            }
            return View(); 
        }
        public IEnumerable<EmployeeRatedSkillsViewModel> GetRatedSkills(int EmpId)
        {
            var RatedSkills = _skillRatingService.GetRatedSkills(EmpId).ToSkillRatedViewmodel();
            return RatedSkills;
        }
        public ActionResult EmployeeRating()
        {
            if (Session["customerId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var EmpId = Convert.ToInt32(Session["customerId"]);
            EmployeeRatingScreenViewModel ratingObj = new EmployeeRatingScreenViewModel();
            ratingObj.RatedSkills = GetRatedSkills(EmpId);
            ratingObj.SkillRatings = EmployeeRatings();
            return View(ratingObj);
        }        
        
        
        public int DeleteRating(int Id)
        {
            
                return _skillRatingService.Delete(Id);
              
        }
        public ActionResult EmployeeProfile()
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
            return View("EmployeeProfile", profile);
        }
    }
}