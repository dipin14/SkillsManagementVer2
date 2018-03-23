﻿using Autofac;
using Autofac.Integration.Mvc;
using Skillset_BLL.Services;
using Skillset_DAL.Repositories;
using System.Reflection;
using System.Web.Mvc;
using System.Web.Routing;

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

            builder.RegisterType<SkillService>().As<ISkillService>().InstancePerRequest();
            builder.RegisterType<SkillRepository>().As<ISkillRepository>().InstancePerRequest();
            
            builder.RegisterControllers(Assembly.GetExecutingAssembly());
            var container = builder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}
