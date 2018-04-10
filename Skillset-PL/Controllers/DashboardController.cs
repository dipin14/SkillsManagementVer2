using Common.DTO;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private IEmployeeServices _services;
        private ISkillRatingService _ratingservices;
        private ISkillService _skillservices;
        public DashboardController(IEmployeeServices services,ISkillRatingService ratingservices,ISkillService skillservices)
        {
            _services = services;
            _ratingservices = ratingservices;
            _skillservices = skillservices;
        }
        // GET: Dashboard
      
        public ActionResult Index()
        {
            IEnumerable l;
            //Get total skills
            ViewBag.TotalSkills = _skillservices.GetSkillsCount();
            ViewBag.TotalEmployees = _services.GetEmployeesCount();

            //Get total ratings performed 
            if ((_skillservices.GetSkillsCount() * _services.GetEmployeesCount()) == 0)
            {
                ViewBag.TotalSkillRatings = 0;
            }
            else
            {
                ViewBag.TotalSkillRatings = ((_ratingservices.GetSkillRatingsCount() * 100) / (_skillservices.GetSkillsCount() * _services.GetEmployeesCount()));
            }
            ViewBag.MaximumRatings = (_skillservices.GetSkillsCount() * _services.GetEmployeesCount());
            ViewBag.TotalSkillRatingsCount = _ratingservices.GetSkillRatingsCount();


            //Get chart data
            ViewBag.SkillnameExcludeList = string.Format("'{0}'", string.Join("','", _ratingservices.GetEmployeeRatedSkillExcludeSpecial().Select(i => i.Replace("'", "\"\"")).ToArray()));
            ViewBag.RatingAverage = _ratingservices.GetRatingAverage();

            //Get table data
            ViewBag.TopSkillsTableData= _services.GetTopRatedRecentEmployees();

            //Get Polar chart data
            ViewBag.TopRatedEmployees = _ratingservices.GetTopEmployeeRating();

            //Get Pie chart data
            ViewBag.LeastRatedEmployees = _ratingservices.GetLeastEmployeeRating();
            return View();
        }
    }
}
