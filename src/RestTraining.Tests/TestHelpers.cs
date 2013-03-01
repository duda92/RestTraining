using System;
using System.Collections.Generic;
using System.Net;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Tests.Utils;
using RestTraining.Domain;

namespace RestTraining.Api.Tests
{
    public static class TestHelpers
    {
        public const string BaseUrl = "http://localhost.:9075";

        public static class BoundedReservationsHotelApiHelper 
        {
            public const string Resource = "/api/BoundedReservationsHotels/";
            
            public static int TestPost(BoundedReservationsHotelDTO hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<BoundedReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static List<BoundedReservationsHotelDTO> TestGet()
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<BoundedReservationsHotelDTO>>(BaseUrl, Resource);
                return responseObj;
            }

            public static BoundedReservationsHotelDTO TestGet(int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<BoundedReservationsHotelDTO>(BaseUrl, Resource + id.ToString());
                return responseObj;
            }

            public static int TestPut(BoundedReservationsHotelDTO hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<BoundedReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static BoundedReservationsHotelDTO CreateRandomBoundedReservationsHotelDTO()
            {
                var hotel = new BoundedReservationsHotel
                {
                    Title = RandomUtils.RandomString(10),
                    Address = RandomUtils.RandomString(10),
                    HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludedItem>
                                    {
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner },
                                        new IncludedItem { Count = 1, IncludeItemType = IncludeItemType.Balcony }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            }
                        }
                    };
                return hotel.ToDTO();
            }
        }

        public static class FreeReservationsHotelApiHelper
        {
            public const string Resource = "/api/FreeReservationsHotels/";
            
            public static int TestPost(FreeReservationsHotelDTO hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<FreeReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static List<FreeReservationsHotelDTO> TestGet()
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<FreeReservationsHotelDTO>>(BaseUrl, Resource);
                return responseObj;
            }

            public static FreeReservationsHotelDTO TestGet(int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<FreeReservationsHotelDTO>(BaseUrl, Resource + id.ToString());
                return responseObj;
            }

            public static int TestPut(FreeReservationsHotelDTO hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<FreeReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static FreeReservationsHotelDTO CreateRandomFreeReservationsHotelDTO()
            {
                var hotel = new FreeReservationsHotel
                {
                    Title = RandomUtils.RandomString(10),
                    Address = RandomUtils.RandomString(10),
                    HotelNumbers = new List<HotelNumber>()
                        {
                            new HotelNumber()
                                {
                                    HotelNumberType = RandomUtils.RandomEnumItem<HotelNumberType>(),
                                    IncludeItems = new List<IncludedItem>
                                        {
                                            new IncludedItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(5)  },
                                            new IncludedItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(5), }
                                        },
                                    WindowViews = new List<WindowView> { new WindowView { Type = WindowViewType.Sea }, new WindowView { Type = WindowViewType.Trash }, new WindowView { Type = WindowViewType.Pool }}
                                }
                        }
                };
                return hotel.ToDTO();
            }
        }

        //public static class ClientsApiHelper
        //{
        //    public const string Resource = "/api/Clients/";

        //    public static int TestPost(ClientDTO clientObj)
        //    {
        //        var responseObj = JsonRequestExecutor.ExecutePost<ClientDTO>(clientObj, BaseUrl, Resource);
        //        return responseObj.Id;
        //    }

        //    public static List<ClientDTO> TestGet()
        //    {
        //        var responseObj = JsonRequestExecutor.ExecuteGet<List<ClientDTO>>(BaseUrl, Resource);
        //        return responseObj;
        //    }

        //    public static ClientDTO TestGet(int id)
        //    {
        //        var responseObj = JsonRequestExecutor.ExecuteGet<ClientDTO>(BaseUrl, (Resource + id.ToString()));
        //        return responseObj;
        //    }

        //    public static int TestPut(ClientDTO clientObj)
        //    {
        //        var responseObj = JsonRequestExecutor.ExecutePut<ClientDTO>(clientObj, BaseUrl, Resource);
        //        return responseObj.Id;
        //    }

        //    public static ClientDTO CreateRandomClientDTO()
        //    {
        //        var client =  new Client { Name = RandomUtils.RandomString(10), PhoneNumber = RandomUtils.RandomString(10) };
        //        return client.ToDTO();
        //    }
        //}

        public static class HotelsApiHelper
        {
            public const string Resource = "/api/Hotels/";

            public static List<HotelDTO> TestGet()
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<HotelDTO>>(BaseUrl, Resource);
                return responseObj;
            }
        }

        public static class HotelNumbersApiHelper
        {
            public const string Resource = "/api/Hotels/{0}/HotelNumbers/";

            public static int TestPost(int hotelId, HotelNumberDTO hotelNumber)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<HotelNumberDTO>(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
                return responseObj.Id;
            }

            public static List<HotelNumberDTO> TestGet(int hotelId)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(Resource, hotelId));
                return responseObj;
            }

            public static HotelNumberDTO TestGet(int hotelId, int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
                return responseObj;
            }

            public static int TestPut(HotelNumberDTO hotelNumber)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<HotelNumberDTO>(hotelNumber, BaseUrl, string.Format(Resource, hotelNumber.HotelId));
                return responseObj.Id;
            }

            public static int TestDelete(int hotelId, int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteDelete<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
                return responseObj.Id;
            }

            public static HotelNumberDTO CreateRandomHotelNumberDTO()
            {
                var hotelNumber = new HotelNumber
                    {
                        HotelNumberType = RandomUtils.RandomEnumItem<HotelNumberType>(),
                        IncludeItems = new List<IncludedItem>
                            {
                                new IncludedItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(10) },
                                new IncludedItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(10) },
                            },
                            WindowViews = new List<WindowView>
                                {
                                    new WindowView { Type = RandomUtils.RandomEnumItem<WindowViewType>() },
                                    new WindowView { Type = RandomUtils.RandomEnumItem<WindowViewType>() }
                                }
                    };
                return hotelNumber.ToDTO();
            }
        }

        public static class BoundedPeriodsApiHelper
        {
            private static double counter = 0;
            private static double offset = 7;
            public const string Resource = "/api/BoundedReservations/{0}/Periods/";

            public static int TestPost(int hotelId, BoundedPeriodDTO boundedPeriod)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<BoundedPeriodDTO>(boundedPeriod, BaseUrl, string.Format(Resource, hotelId));
                
                return responseObj == null ? 0 : responseObj.Id;
            }

            public static List<BoundedPeriodDTO> TestGet(int hotelId)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(Resource, hotelId));
                return responseObj;
            }

            public static BoundedPeriodDTO TestGet(int hotelId, int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<BoundedPeriodDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
                return responseObj;
            }

            public static int TestPut(BoundedPeriodDTO boundedPeriod)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<BoundedPeriodDTO>(boundedPeriod, BaseUrl, string.Format(Resource, boundedPeriod.BoundedReservationsHotelId));
                return responseObj.Id;
            }

            public static BoundedPeriodDTO CreateRandomBoundedPeriodDTO(DateTime beginDate)
            {
                var boundedPeriod = new BoundedPeriod
                {
                    BeginDate = beginDate,
                    EndDate = beginDate.AddDays(7)
                };
                return boundedPeriod.ToDTO();
            }
        }

        public static class FreeBookingApiHelper
        {
            public const string Resource = "api/Booking/FreeReservations/{0}/";

            public static int TestPost(int hotelId, FreeBookingDTO booking, out HttpStatusCode code)
            {
                var responseObj = JsonRequestExecutor.ExecutePost(booking, BaseUrl, string.Format(Resource, hotelId), out code);
                return responseObj == null ? 0 : responseObj.Id;
            }

            public static List<FreeBookingDTO> TestGet(int hotelId, out HttpStatusCode code)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<FreeBookingDTO>>(BaseUrl, string.Format(Resource, hotelId), out code);
                return responseObj;
            }

            public static FreeBookingDTO TestGet(int hotelId, int id, out HttpStatusCode code)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<FreeBookingDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id), out code);
                return responseObj;
            }

            public static int TestPut(FreeBookingDTO boundedPeriod, out HttpStatusCode code)
            {
                var responseObj = JsonRequestExecutor.ExecutePut(boundedPeriod, BaseUrl, string.Format(Resource, boundedPeriod.HotelId), out code);
                return responseObj.Id;
            }

            public static FreeBookingDTO CreateFreeBookingDTO(HotelNumberDTO hotelNumber, DateTime beginDate, DateTime endDate)
            {
                var freeBookingDTO = new FreeBookingDTO
                {
                    BeginDate = beginDate,
                    EndDate = endDate,
                    Client = new ClientDTO
                                 {
                                     Name = RandomUtils.RandomString(10),
                                     PhoneNumber = RandomUtils.RandomString(10)
                                 },
                    HotelId = hotelNumber.HotelId,
                    HotelNumberId = hotelNumber.Id
                };
                return freeBookingDTO;
            }
        }
    }
}
