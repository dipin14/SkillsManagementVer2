﻿using System;
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
    [Authorize(Roles ="Manager,Employee")]
    public class SkillRatingController : Controller
    {

       private readonly ISkillService _skillService;
       private ISkillRatingService _skillRatingService;
       public SkillRatingController(ISkillService skillService, ISkillRatingService skillRatingService)
        {
            _skillService = skillService;
            _skillRatingService = skillRatingService;
        }

      /*  public SkillRatingController(ISkillRatingService skillRatingService)
        {
            _skillRatingService = skillRatingService;
        }*/
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
        public ActionResult RateSkills(List<EmployeeSkillRatingViewModel> ratingList)
        {
           var result= _skillRatingService.Create(ratingList.ToSkillRatingDTOList());
            return View(result);
        } 
    }
}
