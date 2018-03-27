using System;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    public class SkillRatingController : Controller
    {

        private readonly ISkillService _skillService;
        private ISkillRatingService _skillRatingService;
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

        public ActionResult EmployeeRating()
        {
            var skillList = _skillService.GetAllSkills().ToViewModelList();
            return View(skillList);
        }
        public ActionResult RateSkills(List<EmployeeSkillRatingViewModel> ratingList)
        {if (ratingList != null)
            {
                var result = _skillRatingService.Create(ratingList.ToSkillRatingDTOList());

                return View(result);
            }
            return View();
        }

        public ActionResult EmployeeProfile()
        {
            var profile = _skillService.GetProfile(Session["customercode"].ToString()).EmployeeDTOtoViewModel();
            profile.DesignationId = _employeeServices.GetDesignationName(profile.DesignationId);
            profile.RoleId = _employeeServices.GetRoleName(profile.RoleId);
            return View("EmployeeProfile", profile);
        }
    }
}
