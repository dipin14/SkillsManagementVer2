using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;

namespace Skillset_PL.Controllers
{
    /// <summary>
    /// Controller for managing Skills authorized to Admin only
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class SkillController : Controller
    {
        private readonly ISkillService _skillService;

        /// <summary>
        /// Dependency injection for Skill Service
        /// </summary>
        /// <param name="skillService"></param>
        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }

        /// <summary>
        /// Shows list of skills taking pagenumber as argument
        /// </summary>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(int? page)
        {
            //List of skills are ordered by Skill Name
            var skillList = _skillService.GetAllSkills().ToViewModelList().OrderBy(s => s.SkillName);

            //Limited the number of rows per page to 8
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            ViewBag.CurrentPage = pageNumber;
            return View(skillList.ToPagedList(pageNumber, pageSize));
        }

        /// <summary>
        ///  Does GET for Creating Skill
        /// </summary>
        /// <returns></returns>
        public ActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// POST for Creating Skill using SkillViewModel
        /// </summary>
        /// <param name="skillViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Create(SkillViewModel skillViewModel)
        {
            try
            {
                //Leading and trailing empty spaces are trimmed
                skillViewModel.SkillName = skillViewModel.SkillName.Trim();
                skillViewModel.SkillDescription = skillViewModel.SkillDescription.Trim();
                var skillCreateResult = _skillService.Create(skillViewModel.ToDTO());

                //-1 is returned if new Skill has already existing Skill Name
                if (skillCreateResult == -1)
                {
                    ModelState.AddModelError("SkillName", "Skill Name Already Exists");
                    return View(skillViewModel);
                }
                else if (skillCreateResult == 0)
                {
                    ViewBag.Message = "Db Creation Error! Please Restart Application";
                }
                //Notification Message is stored in tempdata
                TempData["message"] = "Successfully Added Skill";
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        /// <summary>
        /// Gets Skill values using SkillName
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        public ActionResult Edit(string skillName, int page)
        {
            //Pass current page to edit view
            ViewBag.CurrentPage = page; 

            if (skillName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Skill values returned from GetBySkillName is assigned to skillView
            SkillViewModel skillView = _skillService.GetBySkillName(skillName).ToViewModel();
            if (skillView == null)
            {
                return HttpNotFound();
            }
            return View(skillView);
        }

        /// <summary>
        /// Updates Skill values with values from SkillViewModel
        /// </summary>
        /// <param name="skillView"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillViewModel skillView, FormCollection collection, int page)
        {
            try
            {
                //Pass current page to edit view
                ViewBag.CurrentPage = page;

                skillView.SkillName = skillView.SkillName.Trim();
                skillView.SkillDescription = skillView.SkillDescription.Trim();
                var skillUpdateResult = _skillService.Update(skillView.ToDTO());

                //-1 is returned if the updated Skill Name already exists
                if (skillUpdateResult == -1)
                {
                    ModelState.AddModelError("SkillName", "Skill Name already exists");
                    return View(skillView);
                }

                //Notification Message is stored in tempdata
                TempData["message"] = "Modified skill record";
                return RedirectToAction("Index", new { page = page });
            }
            catch
            {
                ModelState.AddModelError("SkillName", "Skill Name already exists");
                return View(skillView);
            }
        }

        /// <summary>
        /// GETS Skill values using Skill Name
        /// </summary>
        /// <param name="skillName"></param>
        /// <returns></returns>
        public ActionResult Delete(string skillName)
        {
            if (skillName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            else
            {
                SkillViewModel skillView = _skillService.GetBySkillName(skillName).ToViewModel();
                return View(skillView);
            }
        }

        /// <summary>
        /// Deletes Skill record using Skill Id
        /// </summary>
        /// <param name="skillView"></param>
        /// <param name="collection"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(SkillViewModel skillView, FormCollection collection)
        {
            try
            {
                if (skillView == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                else
                {
                    var deleteResult = _skillService.Delete(skillView.SkillName);
                    //Notification Message is stored in tempdata
                    TempData["message"] = "Successfully deleted skill record";
                    return RedirectToAction("Index");
                }
            }
            catch (Exception)
            {
                return View();
            }
        }
    }
}
