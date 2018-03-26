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

namespace Skillset_PL.Controllers
{
    public class DashboardController : Controller
    {
        private IEmployeeServices _services;
        public DashboardController(IEmployeeServices services)
        {
            _services = services;
        }
        // GET: Dashboard
        [Authorize(Roles ="Admin")]
        public ActionResult Index()
        {
            ViewBag.SkillnameList = "'Java','C','C#','Python'";
            ViewBag.RatingList = "0,90,20,100";
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