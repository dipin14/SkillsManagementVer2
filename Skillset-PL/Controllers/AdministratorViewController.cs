using Common.DTO;
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
    public class AdministratorViewController : Controller
    {
        private readonly IAdminEmployeeSkillService _empSkillService;

        public AdministratorViewController(IAdminEmployeeSkillService skillService)
        {
            _empSkillService = skillService;
        }

        // GET: Employee Details
        public ActionResult Index(string option, string search)
        {
            IEnumerable<AdminEmployeeDTO> employeerecordlist;
            //calling method to search for employee details
            employeerecordlist = _empSkillService.ViewSearchedRecords(option, search);

            List<AdministratorEmployeeViewModel> recordlist = new List<AdministratorEmployeeViewModel>();

            foreach (var obj in employeerecordlist)
            {
                recordlist.Add(new AdministratorEmployeeViewModel(obj.EmployeeCode, obj.Name, obj.Designation));
            }
            return View(recordlist);
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
                recordlist.Add(new AdministratorSkillViewModel(obj.SkillName, obj.SkillValue, obj.RatingDate));
            }
            string employeeName = _empSkillService.FindEmployeeName(id);
            ViewData["employeename"] = employeeName;
            ViewData["employeecode"] = id;
            return View(recordlist);
        }
    }
}