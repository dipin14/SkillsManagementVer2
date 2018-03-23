using Skillset_BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Skillset_PL.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService logService;
        public LoginController()
        {

        }
        public LoginController(ILoginService logSer)
        {
            logService = logSer;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string employeeCode, string password)
        {
            var role = logService.GetRole(employeeCode, password);
            if (role == "Admin")
            {
                return RedirectToAction("Index");
            }
            else if (role == "Manager")
            {
                return RedirectToAction("Search");
            }
            else if (role == "Employee")
            {
                return RedirectToAction("Search");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Employee Code or Password";
                return View();
            }

        }
    }
}