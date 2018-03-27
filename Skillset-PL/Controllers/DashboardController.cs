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
            //ViewBag.SkillnameList = "'Java','C','C#','Python'";
            //ViewBag.RatingList = "0,90,20,100";
            ViewBag.SkillnameList = _services.GetEmployeeRatedSkill();
            IQueryable<string> _barcodes = _services.GetEmployeeRating();
            //foreach (var e in name)
            //{
            //    ViewBag.SkillnameList = string.Format("'{0}'", string.Join("','", e.Select(i => i.Replace("'", "''")).ToArray()));
            //}
            ViewBag.Productname_List = string.Format("'{0}'", string.Join("','", _services.GetEmployeeRatedSkill().Select(i => i.Replace("'", "\"\"")).ToArray()));

            ViewBag.RatingList = string.Format("'{0}'", string.Join("','", _barcodes.Select(i => i.Replace("'", "\"\"")).ToArray()));

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