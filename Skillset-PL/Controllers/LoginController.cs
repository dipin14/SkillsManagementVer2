using Skillset_BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
        [Authorize]
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
                FormsAuthentication.SetAuthCookie(role, false);
                return RedirectToAction("Index");
            }
            else if (role == "Manager")
            {
                FormsAuthentication.SetAuthCookie(role.ToString(), false);
                return RedirectToAction("Search");
            }
            else if (role == "Employee")
            {
                FormsAuthentication.SetAuthCookie(role.ToString(), false);
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