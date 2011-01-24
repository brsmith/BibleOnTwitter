using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using BibleOnTwitter.Infrastructure.Web;
using BibleOnTwitter.Infrastructure.Services;
using BibleOnTwitter.Infrastructure.Tasks;
using System.Threading.Tasks;

namespace BibleOnTwitter.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            var Container = MvcBootstrap.Start();

            Task.Factory.StartNew(() =>
                {
                    var TaskService = Container.Resolve<ITaskService>();
                    TaskService.Start();
                });

            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }        
    }
}