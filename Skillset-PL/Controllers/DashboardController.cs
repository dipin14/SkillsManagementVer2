using Common.DTO;
using Newtonsoft.Json;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Skillset_PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private IEmployeeServices _services;
        public DashboardController(IEmployeeServices services)
        {
            _services = services;
        }
        // GET: Dashboard
      
        public ActionResult Index()
        {
            ViewBag.TotalSkills = _services.GetSkillsCount();
            ViewBag.TotalSkillRatings = _services.GetSkillRatingsCount();
            ViewBag.TotalEmployees = _services.GetEmployeesCount();
           
            ViewBag.SkillnameList = string.Format("'{0}'", string.Join("','", _services.GetEmployeeRatedSkill().Select(i => i.Replace("'", "\"\"")).ToArray()));

            ViewBag.RatingList = _services.GetEmployeeRating();
            var dtoList = _services.GetRecentEmployees();
            var modelList = new List<EmployeeViewModel>();
            foreach (EmployeeDTO item in dtoList)
            {
                modelList.Add(item.EmployeeDTOtoViewModel());
            }
            return View(modelList);
        }
    }
}