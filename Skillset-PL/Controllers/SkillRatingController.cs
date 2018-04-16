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

        /// <summary>
        /// Dependency injection for Skill Service,skillRatingService and employeeServices
        /// </summary>
        /// <param name="skillService"></param>
        /// <param name="skillRatingService"></param>
        ///  <param name="employeeServices"></param>
        ///  <returns></returns>
        public SkillRatingController(ISkillService skillService, ISkillRatingService skillRatingService, IEmployeeServices employeeServices)
        {
            _skillService = skillService;
            _skillRatingService = skillRatingService;
            _employeeServices = employeeServices;
        }


        /// <summary>
        /// Get all The skills from the skill table
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SkillViewModel> EmployeeRatings()
        {
            var skillList = _skillRatingService.GetAllSkills().ToViewModelList();
            return skillList;
        }


        /// <summary>
        /// POST for Creating skill ratings using EmployeeSkillRaingViewModel
        /// </summary>
        /// <param name="ratingList"></param>
        /// <returns></returns>
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


        /// <summary>
        /// get All the ratedSkill List of the logged in employee using EmployeeRatedSkillsViewModel
        /// </summary>
        /// <param name="EmpId"></param>
        /// <returns></returns>
        public IEnumerable<EmployeeRatedSkillsViewModel> GetRatedSkills(int EmpId)
        {
            var RatedSkills = _skillRatingService.GetRatedSkills(EmpId).ToSkillRatedViewmodel();
            return RatedSkills;
        }



        /// <summary>
        /// Get all The skills from the skill table
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Employee")]
        public ActionResult EmployeeRating()
        {
            if (Session["customerId"] == null)
            {
                return RedirectToAction("Login", "Login");
            }
            var EmpId = Convert.ToInt32(Session["customerId"]);
            EmployeeRatingScreenViewModel ratingObj = new EmployeeRatingScreenViewModel();
            var ratedList = GetRatedSkills(EmpId);
            var SkillList= EmployeeRatings();
            //var result = ratedList.Where(x => !SkillList.Contains(x.SkillId));
            var result1 = SkillList.Where(p => !ratedList.Any(p2 => p2.SkillId == p.skillId));
            ratingObj.RatedSkills = ratedList;
           ratingObj.SkillRatings = result1;
            ViewBag.IsSpecial = ratingObj.RatedSkills.ToList().Any(m => m.SkillName == "Special skill");
            return View(ratingObj);

        }

        /// <summary>
        /// Deletes SkillRating record using SkillRatingId
        /// </summary>
        /// <param name="SkillRatingId"></param>
        /// <returns></returns>
        public int DeleteRating(int SkillRatingId)
        {

            return _skillRatingService.Delete(SkillRatingId);

        }

        /// <summary>
        /// Get Employee details of the loggded in employee
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Employee")]
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
