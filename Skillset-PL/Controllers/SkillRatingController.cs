using System;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Linq;
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



        //Action to Get all The skills from the skill table
        public IEnumerable<SkillViewModel> EmployeeRatings()
        {
            var skillList = _skillService.GetAllSkills().ToViewModelList();

            return skillList;

        }

        //Action to Submit All the skill ratings to the skill rating table.Arguments passed list of skill rating objects
        public ActionResult RateSkills(List<EmployeeSkillRatingViewModel> ratingList)
        {

            if (ratingList != null)
            {
                //Other attributes of skillrating object are assigned.
                ratingList.ForEach(m => { m.EmployeeId = Convert.ToInt32(Session["customerId"]); m.Status = true; m.RatingDate = DateTime.Now; });
                var result = _skillRatingService.Create(ratingList.ToSkillRatingDTOList());

                return View(result);
            }
            return View();
        }

        //Action To get All the ratedSkill List of the logged in employee.Parameter being the id of the logged in employee
        public IEnumerable<EmployeeRatedSkillsViewModel> GetRatedSkills(int EmpId)
        {
            var RatedSkills = _skillRatingService.GetRatedSkills(EmpId).ToSkillRatedViewmodel();
            return RatedSkills;
        }

        //The Action on which the first hit occurs and returns the list of skills and skill ratings to the corresponding view
        public ActionResult EmployeeRating()
        {

            var EmpId = Convert.ToInt32(Session["customerId"]);
            EmployeeRatingScreenViewModel ratingObj = new EmployeeRatingScreenViewModel();
            ratingObj.RatedSkills = GetRatedSkills(EmpId);
            ratingObj.SkillRatings = EmployeeRatings();
            ViewBag.IsSpecial = ratingObj.RatedSkills.ToList().Any(m => m.SkillName == "Special skill");
            return View(ratingObj);
        }

        //Action TO delete selected SkillRating.Parameter passed being SkillRatingId
        public int DeleteRating(int SkillRatingId)
        {

            return _skillRatingService.Delete(SkillRatingId);

        }

        //Action to Get the Employee Profile Of the Logged in Employee
        public ActionResult EmployeeProfile()
        {
            var EmployeeDtoList = _employeeServices.GetProfile(Session["customercode"].ToString());
            var profile = EmployeeDtoList.EmployeeDTOtoViewModel();
            Session["customerId"] = EmployeeDtoList.Id;
            profile.DesignationId = _employeeServices.GetDesignationName(profile.DesignationId);
            profile.RoleId = _employeeServices.GetRoleName(profile.RoleId);
            return View("EmployeeProfile", profile);
        }
    }
}