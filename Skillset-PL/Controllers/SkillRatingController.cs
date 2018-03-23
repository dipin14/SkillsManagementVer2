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

        public SkillRatingController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        // GET: Skill
        public ActionResult GetAllSkills()
        {

            return View();
        }

        public ActionResult EmployeeRating()
        {
            var skillList = _skillService.GetAllSkills().ToViewModelList();
            return View( skillList);
        }

    }
}
