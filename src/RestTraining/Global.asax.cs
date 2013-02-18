using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestTraining.Api.Models;
using RestTraining.Domain;

namespace RestTraining.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Database.SetInitializer(new DbInitializer());
            Bootstrapper.Initialise();
        }
    }


    public class DbInitializer : DropCreateDatabaseAlways<RestTrainingApiContext>
    {

        protected override void Seed(RestTrainingApiContext context)
        {
            InitializingByTestValues(context);
        }

        private void InitializingByTestValues(RestTrainingApiContext context)
        {
            IClientRepository repository = new ClientRepository();
            repository.InsertOrUpdate(new Client { Name = "Test", PhoneNumber = "test" });
            repository.Save();
        }
    }
}