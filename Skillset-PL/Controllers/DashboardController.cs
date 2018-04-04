using Common.DTO;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
            //Get total skills
            ViewBag.TotalSkills = _services.GetSkillsCount();
            ViewBag.TotalEmployees = _services.GetEmployeesCount();

            //Get total ratings performed 
            if ((_services.GetSkillsCount() * _services.GetEmployeesCount()) == 0)
            {
                ViewBag.TotalSkillRatings = 0;
            }
            else
            {
                ViewBag.TotalSkillRatings = ((_services.GetSkillRatingsCount() * 100) / (_services.GetSkillsCount() * _services.GetEmployeesCount()));
            }
            ViewBag.MaximumRatings = (_services.GetSkillsCount() * _services.GetEmployeesCount());
            ViewBag.TotalSkillRatingsCount = _services.GetSkillRatingsCount();


            //Get chart data
            ViewBag.SkillnameList = string.Format("'{0}'", string.Join("','", _services.GetEmployeeRatedSkill().Select(i => i.Replace("'", "\"\"")).ToArray()));
            ViewBag.RatingList = _services.GetEmployeeRating();

            ViewBag.SkillnameExcludeList = string.Format("'{0}'", string.Join("','", _services.GetEmployeeRatedSkillExcludeSpecial().Select(i => i.Replace("'", "\"\"")).ToArray()));
            ViewBag.RatingAverage = _services.GetRatingAverage();

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
