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