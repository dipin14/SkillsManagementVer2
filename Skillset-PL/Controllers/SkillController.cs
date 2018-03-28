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
            var skillList = _skillService.GetAllSkills().ToViewModelList().OrderByDescending(s => s.SkillName);

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
                var skillCreateResult = _skillService.Create(skillViewModel.ToDTO());

                if (skillCreateResult == -1)
                {
                    ModelState.AddModelError("SkillName", "Skill Name Already Exists");
                    return View(skillViewModel);
                }
                else if (skillCreateResult == 0)
                {
                    ViewBag.Message = "Db Creation Error! Please Restart Application";
                }
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
                _skillService.Update(skillView.ToDTO());
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("SkillName", "Skillname already exists");
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
