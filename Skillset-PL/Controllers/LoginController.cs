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
        private  ILoginService logService;
        public LoginController(ILoginService logSer)
        {
            logService = logSer;
        }
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        /// <summary>
        ///Login into the system 
        /// </summary>
        /// <returns></returns>
        public ActionResult Login()
        {
            try
            {
                if (Request.IsAuthenticated && (Session["role"].ToString() == "Admin"))
                {
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (Request.IsAuthenticated && (Session["role"].ToString() == "Manager"))
                {
                    return RedirectToAction("Myprofile", "Manager");
                }
                else if (Request.IsAuthenticated && (Session["role"].ToString() == "Employee"))
                {
                    return RedirectToAction("EmployeeProfile", "SkillRating");
                }
            }
            catch 
            {
                return View();
            }
            return View();
            
        }
        /// <summary>
        /// For Login to the system
        /// </summary>
        /// <param name="employeeCode"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Login(string employeeCode, string password)
        {
            if (!(string.IsNullOrEmpty(employeeCode) || string.IsNullOrEmpty(password)))
            {
                employeeCode = employeeCode.Trim();
                password = password.Trim();
                var role = logService.GetRole(employeeCode, password);
                if (role == "Admin")
                {
                    var authTicket = new FormsAuthenticationTicket(1, employeeCode, DateTime.Now, DateTime.Now.AddMinutes(600), false, "Admin");
                    SetCode(authTicket);
                    Session["customercode"] = employeeCode;
                    Session["role"] = "Admin";
                    return RedirectToAction("Index", "Dashboard");
                }
                else if (role == "Manager")
                {
                    var authTicket = new FormsAuthenticationTicket(1, employeeCode, DateTime.Now, DateTime.Now.AddMinutes(600), false, "Manager");
                    SetCode(authTicket);
                    Session["customercode"] = employeeCode;
                    Session["role"] = "Manager";
                    return RedirectToAction("MyProfile", "Manager");
                }
                else if (role == "Employee")
                {
                    var authTicket = new FormsAuthenticationTicket(1, employeeCode, DateTime.Now, DateTime.Now.AddMinutes(600), false, "Employee");
                    SetCode(authTicket);
                    Session["customercode"] = employeeCode;
                    Session["role"] = "Employee";
                    return RedirectToAction("EmployeeProfile", "SkillRating");
                }
                else
                {
                    ViewBag.ErrorMessage = "Invalid Employee Code or Password";
                    return View();
                }
            }
            else
            {
                ViewBag.ErrorMessage = "Please Enter Employee Code and Password";
                return View();
            }
        }
        /// <summary>
        /// Sets authentication cookie
        /// </summary>
        /// <param name="authTicket"></param>
        public void  SetCode(FormsAuthenticationTicket authTicket)
        {
            string encryptedTicket = FormsAuthentication.Encrypt(authTicket);
            var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encryptedTicket);
            System.Web.HttpContext.Current.Response.Cookies.Add(authCookie);
            
        }
        /// <summary>
        ///For logout purpose 
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            Session.Abandon();
            FormsAuthentication.SignOut();          
            return RedirectToAction("Login");
        }
    }
}