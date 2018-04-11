using Autofac;
using Autofac.Integration.Mvc;
using Skillset_BLL.Services;
using Skillset_DAL.ContextClass;
using Skillset_DAL.Repositories;
using System;
using System.Reflection;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Skillset_PL
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            //Registering dependency injection for Autofac
            var builder = new ContainerBuilder();

            // manual registration of types;
            builder.RegisterType<EmployeeServices>().As<IEmployeeServices>();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();

            builder.RegisterType<SkillService>().As<ISkillService>().InstancePerRequest();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>().InstancePerRequest();

            builder.RegisterType<ReportingStaffService>().As<IReportingStaffService>().InstancePerRequest();
            builder.RegisterType<ReportingStaff>().As<IReportingStaffRepository>().InstancePerRequest();
         
            builder.RegisterType<SkillRatingService>().As<ISkillRatingService>().InstancePerRequest();
            builder.RegisterType<SkillRatingRepository>().As<ISkillRatingRepository>().InstancePerRequest();

            builder.RegisterType<LoginService>().As<ILoginService>().InstancePerRequest();
            builder.RegisterType<LoginRepository>().As<ILoginRepository>().InstancePerRequest();

            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null || authCookie.Value == "")
            return;
            FormsAuthenticationTicket authTicket;
            try
            {
                authTicket = FormsAuthentication.Decrypt(authCookie.Value);
            }
            catch
            {
                return;
            }
              
            // retrieve roles from UserData
            string[] roles = authTicket.UserData.Split(';');

            if (Context.User != null)
                Context.User = new GenericPrincipal(Context.User.Identity, roles);
        }
        protected void Application_BeginRequest()
        {	
           Response.Cache.SetCacheability(HttpCacheability.NoCache);	
           Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));	
           Response.Cache.SetNoStore();	
        }
    }
}
