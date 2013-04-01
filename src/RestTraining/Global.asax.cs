using System;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestTraining.Api.Domain;
using RestTraining.Api.Infrastructure;

namespace RestTraining.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DbInitializer());
            Bootstrapper.Initialise();
            GlobalConfiguration.Configuration.Formatters.Insert(0, new JsonMediaTypeFormatter());
        }
    }
}