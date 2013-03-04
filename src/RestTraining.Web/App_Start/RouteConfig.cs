using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestTraining.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Free Booking List",
                url: "FreeBooking/{hotelId}/{action}",
                defaults: new { controller = "FreeBooking" }
            );

            routes.MapRoute(
                name: "Bounded Booking List",
                url: "BoundedBooking/{hotelId}/{action}",
                defaults: new { controller = "BoundedBooking" }
            );

            routes.MapRoute(
                name: "BoundedPeriods List",
                url: "BoundedPeriods/{hotelId}/",
                defaults: new { controller = "BoundedPeriods", action = "Index" }
            );
            
            routes.MapRoute(
                name: "Create BoundedPeriod",
                url: "BoundedPeriods/{hotelId}/Create",
                defaults: new { controller = "BoundedPeriods", action = "Create" }
            );

            routes.MapRoute(
                name: "Edit BoundedPeriod",
                url: "BoundedPeriods/{hotelId}/Edit/{id}",
                defaults: new { controller = "BoundedPeriods", action = "Edit" }
            );

            routes.MapRoute(
                name: "Delete BoundedPeriod",
                url: "BoundedPeriods/{hotelId}/Delete/{id}",
                defaults: new { controller = "BoundedPeriods", action = "Edit" }
            );

            routes.MapRoute(
                name: "Create HotelNumber",
                url: "Hotels/{hotelId}/HotelNumbers/Create",
                defaults: new { controller = "HotelNumbers", action = "Create" }
            );

            routes.MapRoute(
                name: "Edit HotelNumbers",
                url: "Hotels/{hotelId}/HotelNumbers/Edit/{id}",
                defaults: new { controller = "HotelNumbers", action = "Edit" }
            );

            routes.MapRoute(
                name: "Delete HotelNumbers",
                url: "Hotels/{hotelId}/HotelNumbers/Delete/{id}",
                defaults: new { controller = "HotelNumbers", action = "Delete" }
            );

            routes.MapRoute(
                name: "HotelNumbers",
                url: "Hotels/{hotelId}/HotelNumbers/{idfgdfgdfgd}",
                defaults: new { controller = "HotelNumbers", action = "Index", idfgdfgdfgd = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "RestTraining.Web.Controllers" }
            );
        }
    }
}