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
    [Authorize(Roles = "Admin")]
    public class SkillController : Controller
    {
        private readonly ISkillService _skillService;

        public SkillController(ISkillService skillService)
        {
            _skillService = skillService;
        }
        
        // GET: Skill
        public ActionResult Index(int? page)
        {
            //List of skills are ordered by Skill Name
            var skillList = _skillService.GetAllSkills().ToViewModelList().OrderBy(s => s.SkillName);

            //Limited the number of rows per page to 3
            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(skillList.ToPagedList(pageNumber, pageSize));
        }

        // GET: Skill/Details/5
        public ActionResult Details(string name)
        {
            return View();
        }

        // GET: Skill/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Skill/Create
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

        // GET: Skill/Edit/5
        public ActionResult Edit(string skillName)
        {
            if (skillName == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SkillViewModel skillView = _skillService.GetBySkillName(skillName).ToViewModel();
            if (skillView == null)
            {
                return HttpNotFound();
            }
            return View(skillView);
        }

        // POST: Skill/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SkillViewModel skillView, FormCollection collection)
        {
            try
            {
                skillView.SkillName = skillView.SkillName.Trim();
                skillView.SkillDescription = skillView.SkillDescription.Trim();
                var skillUpdateResult = _skillService.Update(skillView.ToDTO());
                //-1 is returned if the updated Skill Name already exists
                if(skillUpdateResult == -1)
                {
                    ModelState.AddModelError("SkillName", "Skill Name already exists");
                    return View(skillView);
                }
                //Notification Message is stored in tempdata
                TempData["message"] = "Modified skill record";
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("SkillName", "Skill Name already exists");
                return View(skillView);
            }
        }

        // GET: Skill/Delete/5
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

        // POST: Skill/Delete/5
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
