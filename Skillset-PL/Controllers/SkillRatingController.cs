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
    [Authorize(Roles ="Employee,Manager")]
    public class SkillRatingController : Controller
    {

        private readonly ISkillService _skillService;
        private ISkillRatingService _skillRatingService;

        public SkillRatingController(ISkillService skillService, ISkillRatingService skillRatingService)
        {
            _skillService = skillService;
            _skillRatingService = skillRatingService;
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
        { if (ratingList != null)
            {
                ratingList.ForEach(m => m.EmployeeId = Convert.ToInt32(Session["customerId"]));
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

            var EmpId = Convert.ToInt32(Session["customerId"]);

            EmployeeRatingScreenViewModel ratingObj = new EmployeeRatingScreenViewModel();
            ratingObj.RatedSkills = GetRatedSkills(EmpId);
            ratingObj.SkillRatings = EmployeeRatings();
            return View(ratingObj);
        }
        public ActionResult EmployeeProfile()
        {
            var profile = _skillService.GetProfile(Session["customercode"].ToString()).EmployeeDTOtoViewModel();
            Session["customerId"] = profile.EmployeeId;
            return View("EmployeeProfile", profile);
        }
    }
    }