using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestTraining.Api.DTO;
using RestTraining.Web.Binders;

namespace RestTraining.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            BootstrapSupport.BootstrapBundleConfig.RegisterBundles(BundleTable.Bundles);
            ModelBinders.Binders.Add(typeof(HotelNumberDTO), new HotelNumberDTOBinder());
            ModelBinders.Binders.Add(typeof(HotelDTO), new HotelDTOBinder());
            ModelBinders.Binders.Add(typeof(BoundedBookingDTO), new BookingDTOBinder());
            ModelBinders.Binders.Add(typeof(FreeBookingDTO), new BookingDTOBinder());
            //ModelBinders.Binders.Add(typeof(FreeBookingDTO), new DateTimeBinder());
            
        }
    }
}