using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestTraining.Common.DTO;
using RestTraining.Web.Binders;
using System;

namespace RestTraining.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static string publicKey = "<RSAKeyValue><Modulus>x2oPfv9IJkkzsJF8c9k3fmY+ZEGQ9tpQjNcXDBGBePqVS5RzoGbh6HL+/WhqIXh65KNC1Q9VlgaPi01TIpovUvgSOF5tMIqcjPLMkaFaJl8vc7UVh8t/RA0YHeS7ePLP0XFTpmKURhtCO8lPsdO/NeKh+mEpYtelUQUc23srjnrSQKQO6mMGYp9wF24RoTz+i1hEj9/bS5qfJG0P1YuBhAn7PKRaj8DKuykU/YiZhepcSOuyPO+H/6C+9FPwyVzjm2ZkAy0Ju4LIB8k/5T7Yph1VGfYL7EK0VNa3fK0QrFlSkqQeicYpu4afO5PaG/CN5svcEecM2TtMS2YmMGQQVQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string appId = "1";

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
        }
    }


}