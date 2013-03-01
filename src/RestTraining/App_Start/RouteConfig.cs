﻿using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace RestTraining.Api
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapHttpRoute(
                name: "Booking",
                routeTemplate: "api/Booking/FreeReservations/{hotelId}/{id}",
                defaults: new { controller = "FreeReservationsBooking", id = RouteParameter.Optional }
            );

            routes.MapHttpRoute(
                name: "Test",
                routeTemplate: "api/Hotels/{hotelId}/HotelNumbers/{id}",
                defaults: new { controller = "HotelNumbers", id = RouteParameter.Optional }
            );

            routes.MapHttpRoute(
                name: "Periods",
                routeTemplate: "api/BoundedReservations/{hotelId}/Periods/{id}",
                defaults: new { controller = "BoundedPeriods", id = RouteParameter.Optional }
            );
            
            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional, hotelId = RouteParameter.Optional }
            ); 
            
            routes.MapRoute( "Default5", "{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "RestTraining.Api.Controllers" }
            );
        }
    }
}