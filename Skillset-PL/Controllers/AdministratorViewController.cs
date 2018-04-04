using Common.DTO;
using PagedList;
using Skillset_BLL.Services;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdministratorViewController : Controller
    {
        private readonly IAdminEmployeeSkillService _empSkillService;

        public AdministratorViewController(IAdminEmployeeSkillService skillService)
        {
            _empSkillService = skillService;
        }

        // GET: Searched Employee Details
        public ActionResult Index(string search,int? pageNo)
        {
            var pageNumber = (pageNo ?? 1)-1;
            var totalCount = 0;
            var pageSize = 3;
            ViewBag.search = search;
            IEnumerable<AdminEmployeeDTO> employeerecordlist;
            //calling method to search for employee details
            employeerecordlist = _empSkillService.ViewSearchedRecords(search,pageNumber, pageSize, out totalCount);

            List<AdministratorEmployeeViewModel> recordlist = new List<AdministratorEmployeeViewModel>();
            foreach (var obj in employeerecordlist)
            {
                recordlist.Add(new AdministratorEmployeeViewModel(obj.EmployeeCode, obj.Name, obj.Designation));
            }
            IPagedList<AdministratorEmployeeViewModel> pageOrders = new StaticPagedList<AdministratorEmployeeViewModel>(recordlist, pageNumber + 1, 3, totalCount);
            return View(pageOrders);
        }

        // GET: Employee Skills Details      
        public ActionResult Skills(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            IEnumerable<AdminSkillDTO> skillrecordlist;
            //calling method to get skill details of a particular employee
            skillrecordlist = _empSkillService.GetSkillDetails(id);

            List<AdministratorSkillViewModel> recordlist = new List<AdministratorSkillViewModel>();
            foreach (var obj in skillrecordlist)
            {
                recordlist.Add(new AdministratorSkillViewModel(obj.SkillName, obj.SkillValue, obj.RatingDate,obj.Note));
            }
            string employeeName = _empSkillService.FindEmployeeName(id);
            ViewData["employeename"] = employeeName;
            ViewData["employeecode"] = id;
            return View(recordlist);
        }
    }
}