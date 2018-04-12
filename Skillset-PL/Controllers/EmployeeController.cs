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
    [Authorize(Roles = "Admin")]
    public class EmployeeController : Controller
    {
        private readonly IEmployeeServices _services;
        /// <summary>
        /// Dependency injection for Employee Services
        /// </summary>
        /// <param name="services"></param>
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
            //Get the list of designations with id and name
            list = _services.GetDesignations();
            //Sets id as value and name as text for SelectListItem
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
            //Get the list of qualifications with id and name
            list = _services.GetQualifications();
            //Sets id as value and name as text for SelectListItem
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
            //Get the list of qualifications with id and name
            list = _services.GetManagers();
            //Sets id as value and name as text for SelectListItem
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
            //Get the list of qualifications with id and name
            list = _services.GetRoles();
            //Sets id as value and name as text for SelectListItem
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
        /// Displays list of employees according to search key with pagination.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="page"></param>
        /// <returns></returns>
        public ActionResult Index(string search, int? page)
        {
            var pageNumber = (page ?? 1) - 1;
            var totalCount = 0;
            var pageSize = 8;
            if (search != null)
                search = search.Trim();
            ViewBag.search = search;
            var dtoList = _services.ViewSearchRecords(search, pageNumber, pageSize, out totalCount);
            var modelList = new List<EmployeeViewModel>();
            //Converts and add each dto item to viewmodel
            foreach (EmployeeDTO item in dtoList)
            {             
                item.DesignationId = _services.GetDesignationName(item.DesignationId);
                item.QualificationId = _services.GetQualificationName(item.QualificationId);
                item.RoleId = _services.GetRoleName(item.RoleId);
                item.EmployeeId = _services.GetManagerName(item.EmployeeId);
                modelList.Add(item.EmployeeDTOtoViewModel());
            }
            IPagedList<EmployeeViewModel> pageOrders = new StaticPagedList<EmployeeViewModel>(modelList, pageNumber + 1, pageSize, totalCount);
            return View(pageOrders);
        }
           
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
            employee.Name = employee.Name.Trim();
            employee.Email = employee.Email.Trim();
            employee.Address = employee.Address.Trim();
            employee.MobileNumber =Convert.ToDouble( employee.MobileNumber.ToString().Trim());
            if (ModelState.IsValid)
            {
                int status = _services.AddNewEmployee(employee.EmployeeViewModeltoDTO());
                //If employee code already exists
                if (status == 1)
                {
                    ModelState.AddModelError("EmployeeCode", "Employee code exists");

                }
                //If mobile number already exists 
                else if (status == 2)
                {
                    ModelState.AddModelError("MobileNumber", "Mobile number already exists");
                }
                //If email already exists
                else if (status == 3)
                {
                    ModelState.AddModelError("Email", "Email already exists");
                }
                //If some error occur during creation of employee record
                else if (status == -1)
                {
                    TempData["error"] = "Error in creating new Employee record";

                }
                //If employee is successfully added
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
                TempData["error"] = "Error in deleting Employee record";
                return RedirectToAction("Delete", id);
            }
            if(status==2)
            {
                TempData["error"] = "Employee record cannot be deleted";
            }
            if(status==1)
            {
                TempData["message"] = "Successfully deleted Employee record";
            }
            return RedirectToAction("Index");
        }

        // GET: Employees/Details/{id}
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
            EmployeeViewModel employeeDetails = employee.EmployeeDTOtoViewModel();
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
            employee.MobileNumber = Convert.ToDouble(employee.MobileNumber.ToString().Trim());

            EmployeeViewModel originalEmployee = (EmployeeViewModel)Session["EmployeeOriginal"];
            var comparer = new ObjectsComparer.Comparer<EmployeeViewModel>();
            if (ModelState.IsValid)
            {

                bool isEqual = comparer.Compare(employee, originalEmployee);
                ViewData["Designations"] = GetDesignations();
                ViewData["Qualifications"] = GetQualifications();
                ViewData["Managers"] = GetManagers();
                ViewData["Roles"] = GetRoles();
                if (!isEqual)
                {
                    int status = _services.EditEmployeeById(employee.EmployeeViewModeltoDTO());
                    //If mobile number already exists 
                    if (status == 1)
                    {
                        ModelState.AddModelError("MobileNumber", "Mobile number already exists");

                    }
                    //If email already exists
                    else if (status == 2)
                    {
                        ModelState.AddModelError("Email", "Email already exists");

                    }
                    else
                    {
                        if (status == -1)
                        {
                            TempData["error"] = "Error in modifying Employee record";

                        }
                        if (status == 0)
                        {
                            TempData["message"] = "Modified Employee record";
                        }

                        if (status == 3)
                        {
                            TempData["error"] = "Role for this employee cannot be changed";
                        }
                        return RedirectToAction("Index");
                    }
                   
                }
                else
                {
                    TempData["error"] = "No modification applied";
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
