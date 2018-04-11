using Common.DTO;
using ObjectsComparer;
using PagedList;
using Skillset_BLL.Services;
using Skillset_PL.ViewModelExtensions;
using Skillset_PL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace Skillset_PL.Controllers
{
    [Authorize(Roles ="Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _services;
 
        public EmployeeController(IEmployeeServices services)
        {
            _services = services;
           
        }

        /// <summary>
        /// Retrieves the list of Designations
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves the list of Qualifications
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Retrieves the list of managers
        /// </summary>
        /// <returns></returns>
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
        
        /// <summary>
        /// Retrieves the list of Roles
        /// </summary>
        /// <returns></returns>
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
        public ActionResult Index(string search,int? page)
        {
            ViewData["EmployeeCount"] = _services.GetEmployeesCount();
            var pageNumber = (page ?? 1) - 1;
            var totalCount = 0;
            var pageSize = 8;
            TempData["page"] = pageNumber + 1;
            if(search!=null)
                search = search.Trim();
            ViewBag.search = search;
            //get the list of employees according to the search key
            var dtoList = _services.ViewSearchRecords(search,pageNumber,pageSize,out totalCount);
            var modelList = new List<EmployeeViewModel>();
            foreach (EmployeeDTO item in dtoList)
            {
                //get designation name corresponding to id
                item.DesignationId = _services.GetDesignationName(item.DesignationId);
                //get qualification name corresponding to id
                item.QualificationId = _services.GetQualificationName(item.QualificationId);
                //get role name corresponding to id
                item.RoleId = _services.GetRoleName(item.RoleId);
                //get manager name corresponding to id
                item.EmployeeId = _services.GetManagerName(item.EmployeeId);
                modelList.Add(item.EmployeeDTOtoViewModel());
            }
            IPagedList<EmployeeViewModel> pageOrders = new StaticPagedList<EmployeeViewModel>(modelList, pageNumber + 1, pageSize, totalCount);
            return View(pageOrders);
        }
        //  GET: Employees/Create  
        public ActionResult Create()
        {
            ViewData["Designations"] = GetDesignations();
            ViewData["Qualifications"] = GetQualifications();
            ViewData["Managers"] = GetManagers();
            ViewData["Roles"] = GetRoles();
            return View();
        }
        // POST: Employees/Create/{employee}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(EmployeeViewModel employee)
        {
            employee.Name= employee.Name.Trim();
            employee.Email=employee.Email.Trim();
            employee.Address=employee.Address.Trim();
            if (ModelState.IsValid)
            {               
                int status = _services.AddNewEmployee(employee.EmployeeViewModeltoDTO());
                if (status == 1)
                {
                    ModelState.AddModelError("EmployeeCode", "Employee code exists");

                }
                else if (status == 2)
                {
                    ModelState.AddModelError("MobileNumber", "Mobile number already exists");                 
                }
                else if (status == 3)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                }
                else if (status == -1)
                {
                    TempData["message"] = "Error in creating new Employee record";

                }
                else
                {
                    TempData["message"] = "Successfully added Employee record";                  
                    return RedirectToAction("Index");
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

        // POST: Employees/Delete/{id}
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            int status = _services.DeleteEmployeeById(id);
            if (status == 0)
            {
                TempData["message"] = "Error in deleting Employee record";
                return RedirectToAction("Delete", id);
            }
            TempData["message"] = "Successfully deleted Employee record";
            return RedirectToAction("Index");
        }

        // GET: Employees/Details/{id}
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //get employee details for a particular employee code
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

        // GET: Employees/Edit/{id}
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
            EmployeeViewModel employeeDetails= employee.EmployeeDTOtoViewModel();
            Session["EmployeeOriginal"] = employeeDetails;
            return View(employeeDetails);
        }

        // POST: Employees/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(EmployeeViewModel employee)
        {
            employee.Name = employee.Name.Trim();
            employee.Email = employee.Email.Trim();
            employee.Address = employee.Address.Trim();
            EmployeeViewModel originalEmployee=(EmployeeViewModel)Session["EmployeeOriginal"];
            var comparer = new ObjectsComparer.Comparer<EmployeeViewModel>();
            if (ModelState.IsValid)
            {
                
                bool isEqual = comparer.Compare(employee, originalEmployee);
                if (!isEqual)
                {
                    int status = _services.EditEmployeeById(employee.EmployeeViewModeltoDTO());
                    if (status == 0)
                    {
                        TempData["message"] = "Error in modifying Employee record";
                        return RedirectToAction("Edit", employee);
                    }
                    TempData["message"] = "Modified Employee record";
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["message"] = "No modification applied";
                    return RedirectToAction("Index");
                }           
            }
            return View(employee);
           
        }

        // GET: Employee Skills Details      
        public ActionResult Skills(string id, int? page)
        {
            if (id == null)
            {
                return RedirectToAction("Login", "Login");
            }
            IEnumerable<AdminSkillDTO> skillrecordlist;
            //calling method to get skill details of a particular employee
            skillrecordlist = _services.GetSkillDetails(id);

            List<AdministratorSkillViewModel> recordlist = new List<AdministratorSkillViewModel>();
            foreach (var obj in skillrecordlist)
            {
                recordlist.Add(new AdministratorSkillViewModel(obj.SkillName, obj.SkillValue, obj.RatingDate, obj.Note, obj.RatingNote));
            }
            string employeeName = _services.GetEmployeeName(id);
            ViewData["employeename"] = employeeName;
            ViewData["employeecode"] = id;
            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(recordlist.ToPagedList(pageNumber, pageSize));
        }

    }
}
