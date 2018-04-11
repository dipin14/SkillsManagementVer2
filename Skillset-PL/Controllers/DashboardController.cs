using Skillset_BLL.Services;
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
            //Get total skills,ratings and employees
            ViewBag.TotalSkills = _skillservices.GetSkillsCount();
            ViewBag.TotalEmployees = _services.GetEmployeesCount();
            ViewBag.TotalSkillRatingsCount = _ratingservices.GetSkillRatingsCount();

            //Get chart data
            ViewBag.SkillnameExcludeList = string.Format("'{0}'", string.Join("','", _ratingservices.GetEmployeeRatedSkillExcludeSpecial().Select(i => i.Replace("'", "\"\"")).ToArray()));
            ViewBag.RatingAverage = _ratingservices.GetRatingAverage();

            //Get table data
            ViewBag.TopSkillsTableData= _services.GetTopRatedRecentEmployees();

            return View();
        }
    }
}
