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
                var authTicket = new FormsAuthenticationTicket( 1,employeeCode,DateTime.Now,DateTime.Now.AddMinutes(10),false,"Admin");
                SetCode(authTicket);
                Session["customercode"] = employeeCode;
                return RedirectToAction("Index", "Dashboard");
            }
            else if (role == "Manager")
            {
                var authTicket = new FormsAuthenticationTicket(1, employeeCode, DateTime.Now, DateTime.Now.AddMinutes(10), false, "Manager");
                SetCode(authTicket);
                Session["customercode"] = employeeCode;
                return RedirectToAction("MyProfile", "Manager");            }
            else if (role == "Employee")
            {
                var authTicket = new FormsAuthenticationTicket(1, employeeCode, DateTime.Now, DateTime.Now.AddMinutes(10), false, "Employee");
                SetCode(authTicket);
                Session["customercode"] = employeeCode;
                return RedirectToAction("EmployeeProfile", "SkillRating");
            }
            else
            {
                ViewBag.ErrorMessage = "Invalid Employee Code or Password";
                return View();
            } 
        }
        public void  SetCode(FormsAuthenticationTicket authTicket)
        {
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
            
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            //Session.Abandon();
            return RedirectToAction("Login");
        }
    }
}