using Common.DTO;
using Skillset_BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    public class EmployeeController : Controller
    {
        private IEmployeeServices _services;
        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
        }
        public List<SelectListItem> GetDesignations()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var list = new List<DesignationDTO>();
            list = _services.GetDesignations();
            for (int i = 0; i < list.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = list[i].Name,
                    Value = list[i].Id.ToString()
                });
            }
            return items;
        }
        public List<SelectListItem> GetQualifications()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var list = new List<QualificationDTO>();
            list = _services.GetQualifications();
            for (int i = 0; i < list.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = list[i].Name,
                    Value = list[i].Id.ToString()
                });
            }
            return items;
        }
        public List<SelectListItem> GetManagers()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var list = new List<EmployeeDTO>();
            list = _services.GetManagers();
            for (int i = 0; i < list.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = list[i].Name,
                    Value = list[i].Id.ToString()
                });
            }
            return items;
        }
        public List<SelectListItem> GetRoles()
        {
            List<SelectListItem> items = new List<SelectListItem>();
            var list = new List<RoleDTO>();
            list = _services.GetRoles();
            for (int i = 0; i < list.Count; i++)
            {
                items.Add(new SelectListItem
                {
                    Text = list[i].Name,
                    Value = list[i].Id.ToString()
                });
            }
            return items;
        }
        // GET: Employee
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            ViewData["Designations"] = GetDesignations();
            ViewData["Qualifications"] = GetQualifications();
            ViewData["Managers"] = GetManagers();
            ViewData["Roles"] = GetRoles();
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeCode,Name,DateOfJoining,DesignationId,QualificationId,RoleId,Experience,DateOfBirth,EmployeeId,Address,Email,MobileNumber,Gender")] EmployeeDTO employee)
        {
            if (ModelState.IsValid)
            {
                int status = _services.AddNewEmployee(employee);
                if (status == 1)
                {
                    ModelState.AddModelError("Employee Code", "Employee code exists");
                }
                else if (status == 2)
                {
                    ModelState.AddModelError("Mobile Number", "Mobile number already exists");
                }
                else if (status == 3)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                }
                else if (status == -1)
                {
                    ViewBag.message = "Error in creating new employee";

                }
                else
                {
                    ViewBag.message = "Successfully Added Employee";
                    return View();
                }
            }
            ViewData["Designations"] = GetDesignations();
            ViewData["Qualifications"] = GetQualifications();
            ViewData["Managers"] = GetManagers();
            ViewData["Roles"] = GetRoles();
            return View(employee);
        }

    }
}