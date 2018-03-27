using Common.DTO;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
            var dtoList = _services.GetAllEmployees();
            var modelList = new List<EmployeeViewModel>();
            foreach (EmployeeDTO item in dtoList)
            {
                item.DesignationId = _services.GetDesignationName(item.DesignationId);
                item.QualificationId = _services.GetQualificationName(item.QualificationId);
                item.RoleId = _services.GetRoleName(item.RoleId);
                item.EmployeeId = _services.GetManagerName(item.EmployeeId);
                modelList.Add(item.EmployeeDTOtoViewModel());
            }
            return View(modelList);
        }
        [HttpPost]
        public ActionResult IndexSearch(string option, string search)
        {
            
            //calling method to search for employee details
            var dtoList = _services.ViewSearchRecords(option, search);
           
                var modelList = new List<EmployeeViewModel>();
                List<AdministratorEmployeeViewModel> recordlist = new List<AdministratorEmployeeViewModel>();

                foreach (EmployeeDTO item in dtoList)
                {
                    item.DesignationId = _services.GetDesignationName(item.DesignationId);
                    item.QualificationId = _services.GetQualificationName(item.QualificationId);
                    item.RoleId = _services.GetRoleName(item.RoleId);
                    item.EmployeeId = _services.GetManagerName(item.EmployeeId);
                    modelList.Add(item.EmployeeDTOtoViewModel());
                }
                return View("Index",modelList);
          
            
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
        public ActionResult Create(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                int status = _services.AddNewEmployee(employee.EmployeeViewModeltoDTO());
                if (status == 1)
                {
                    ViewBag.message = "Employee code exists";
                }
                else if (status == 2)
                {
                    ViewBag.message = "Mobile number already exists";
                }
                else if (status == 3)
                {
                    ViewBag.message = "Email already exists";
                }
                else if (status == -1)
                {
                    ViewBag.message = "Error in creating new employee";

                }
                else
                {
                    ViewBag.message = "Successfully Added Employee";
                    ViewData["Designations"] = GetDesignations();
                    ViewData["Qualifications"] = GetQualifications();
                    ViewData["Managers"] = GetManagers();
                    ViewData["Roles"] = GetRoles();
                    ModelState.Clear();
                    return View();
                }
            }
            ViewData["Designations"] = GetDesignations();
            ViewData["Qualifications"] = GetQualifications();
            ViewData["Managers"] = GetManagers();
            ViewData["Roles"] = GetRoles();
            return View(employee);
        }

        // GET: Employees/Delete/
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            EmployeeDTO employee = _services.GetEmployeeDetailsById(id);
            employee.DesignationId = _services.GetDesignationName(employee.DesignationId);
            employee.QualificationId = _services.GetQualificationName(employee.QualificationId);
            employee.RoleId = _services.GetRoleName(employee.RoleId);
            employee.EmployeeId = _services.GetManagerName(employee.EmployeeId);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee.EmployeeDTOtoViewModel());
        }

        // POST: Employees/Delete/
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            int status = _services.DeleteEmployeeById(id);
            if (status == 0)
            {
                ViewBag.message = "Error in deleting employee record";
                return RedirectToAction("Delete", id);
            }

            return RedirectToAction("Index");
        }

        // GET: Employees/Details/
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDTO employee = _services.GetEmployeeDetailsById(id);
            employee.DesignationId = _services.GetDesignationName(employee.DesignationId);
            employee.QualificationId = _services.GetQualificationName(employee.QualificationId);
            employee.RoleId = _services.GetRoleName(employee.RoleId);
            employee.EmployeeId = _services.GetManagerName(employee.EmployeeId);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee.EmployeeDTOtoViewModel());
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeDTO employee = _services.GetEmployeeDetailsById(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewData["Designations"] = GetDesignations();
            ViewData["Qualifications"] = GetQualifications();
            ViewData["Managers"] = GetManagers();
            ViewData["Roles"] = GetRoles();
            return View(employee.EmployeeDTOtoViewModel());
        }

        // POST: Employees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel employee)
        {
            if (ModelState.IsValid)
            {
                int status = _services.EditEmployeeById(employee.EmployeeViewModeltoDTO());
                if (status == 0)
                {
                    ViewBag.message = "Error in modifying employee record";
                    return RedirectToAction("Edit", employee);
                }
                return RedirectToAction("Index");
            }
            return View(employee);
           
        }

    }
}