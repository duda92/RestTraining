﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using RestTraining.Api.Domain;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Api.Domain.Services;
using RestTraining.Api.Infrastructure;
using RestTraining.Domain;

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


    public class DbInitializer : DropCreateDatabaseAlways<RestTrainingApiContext>
    {

        protected override void Seed(RestTrainingApiContext context)
        {
            InitializingByTestValues(context);
        }

        private void InitializingByTestValues(RestTrainingApiContext context)
        {
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Pool});
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Sea });
            context.WindowViews.Add(new WindowView { Type = WindowViewType.Trash });
            context.SaveChanges();

            IClientRepository repository = new ClientRepository();
            repository.InsertOrUpdate(new Client { Name = "Test", PhoneNumber = "test" });
            repository.Save();

            var boundedReservationsHotel = new BoundedReservationsHotel
            {
                Title = "Test",
                Address = "Test",
                HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            }
                    }
           };

            var freeReservationsHotel = new FreeReservationsHotel
            {
                Title = "Test",
                Address = "Test",
                HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            }
                    }
            };
            IBoundedReservationsHotelRepository boundedReservationsHotelRepository = new BoundedReservationsHotelRepository(new HotelNumbersUpdateService());
            boundedReservationsHotelRepository.InsertOrUpdate(boundedReservationsHotel);
            boundedReservationsHotelRepository.Save();

            IFreeReservationsHotelRepository freeReservationsHotelRepository = new FreeReservationsHotelRepository(new HotelNumbersUpdateService());
            freeReservationsHotelRepository.InsertOrUpdate(freeReservationsHotel);
            freeReservationsHotelRepository.Save();

            context.SaveChanges();
        }

    }
}