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
                var authTicket = new FormsAuthenticationTicket(1, employeeCode, DateTime.Now, DateTime.Now.AddMinutes(20), false, "Admin;Manager;Employee");
                string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
                System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
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