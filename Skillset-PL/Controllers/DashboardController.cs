using Skillset_BLL.Services;
using System.Linq;
using System.Web.Mvc;
using Skillset_PL.ViewModels;

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
      
        public ActionResult Index(string search)
        {
            //Search employee/skill from top rated employee table
            if (search != null)
                search = search.Trim();
           
            //Get total skills,ratings and employees
            ViewBag.TotalSkills = _skillservices.GetSkillsCount();
            ViewBag.TotalEmployees = _services.GetEmployeesCount();
            ViewBag.TotalSkillRatingsCount = _ratingservices.GetSkillRatingsCount();

            //Get chart data excluding special skill
            ViewBag.SkillnameExcludeList = string.Format("'{0}'", string.Join("','", _ratingservices.GetEmployeeRatedSkillExcludeSpecial().Select(i => i.Replace("'", "\"\"")).ToArray()));
            ViewBag.RatingAverage = _ratingservices.GetRatingAverage();

            //Get table data
              ViewBag.TopSkillsTableData = (from b in _services.GetTopRatedRecentEmployees(search) group b by b.Value into g select new Group<string, string> { Key = g.Key, Values = g.Select(s=>s.Key).ToList() }).ToList();

            return View();
        }
    }
}
