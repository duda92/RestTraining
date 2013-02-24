using System.Web.Http;
using Microsoft.Practices.Unity;
using RestTraining.Api.Models;

namespace RestTraining.Api
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();
            GlobalConfiguration.Configuration.DependencyResolver = new Unity.WebApi.UnityDependencyResolver(container);
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            container.RegisterType<IClientRepository, ClientRepository>();
            container.RegisterType<IBoundedReservationsHotelRepository, BoundedReservationsHotelRepository>();
            container.RegisterType<IFreeReservationsHotelRepository, FreeReservationsHotelRepository>();
            container.RegisterType<IHotelRepository, HotelRepository>();
            container.RegisterType<IHotelNumbersRepository, HotelNumbersRepository>();
            container.RegisterType<IBoundedPeriodRepository, BoundedPeriodRepository>();
            
            return container;
        }
    }
}