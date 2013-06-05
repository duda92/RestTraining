using System;
using System.Collections.Generic;
using System.Net;
using RestTraining.Common.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Tests.Utils;
using RestTraining.Common.Proxy;
using RestTraining.Domain;

namespace RestTraining.Api.Tests
{
    public static class TestHelpers
    {
        public const string BaseUrl = "http://localhost.:9075";

        public const string testLogin = "testClient1";
        public const string testPassword = "testClient1";
        public static string publicKey = "<RSAKeyValue><Modulus>x2oPfv9IJkkzsJF8c9k3fmY+ZEGQ9tpQjNcXDBGBePqVS5RzoGbh6HL+/WhqIXh65KNC1Q9VlgaPi01TIpovUvgSOF5tMIqcjPLMkaFaJl8vc7UVh8t/RA0YHeS7ePLP0XFTpmKURhtCO8lPsdO/NeKh+mEpYtelUQUc23srjnrSQKQO6mMGYp9wF24RoTz+i1hEj9/bS5qfJG0P1YuBhAn7PKRaj8DKuykU/YiZhepcSOuyPO+H/6C+9FPwyVzjm2ZkAy0Ju4LIB8k/5T7Yph1VGfYL7EK0VNa3fK0QrFlSkqQeicYpu4afO5PaG/CN5svcEecM2TtMS2YmMGQQVQ==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>";
        public static string appId = "1";

        public static JsonRequestExecutor executor = new JsonRequestExecutor(testLogin, testPassword, publicKey, appId);

        public static class BoundedReservationsHotelApiHelper 
        {
            public const string Resource = "/api/BoundedReservationsHotels/";
            
            public static int TestPost(BoundedReservationsHotelDTO hotelObj)
            {
                var responseObj = executor.ExecutePost<BoundedReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static List<BoundedReservationsHotelDTO> TestGet()
            {
                var responseObj = executor.ExecuteGet<List<BoundedReservationsHotelDTO>>(BaseUrl, Resource);
                return responseObj;
            }

            public static BoundedReservationsHotelDTO TestGet(int id)
            {
                var responseObj = executor.ExecuteGet<BoundedReservationsHotelDTO>(BaseUrl, Resource + id.ToString());
                return responseObj;
            }

            public static int TestPut(BoundedReservationsHotelDTO hotelObj)
            {
                var responseObj = executor.ExecutePut<BoundedReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
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
                        },
                    HotelsAttractions = new List<HotelsAttraction>
                    {
                        new HotelsAttraction
                        {
                            Count = 2, HotelsAttractionType = HotelsAttractionType.SwimmingPool
                        },
                        new HotelsAttraction
                        {
                            Count = 2, HotelsAttractionType = HotelsAttractionType.WaterSlides
                        }
                    },
                    };
                return hotel.ToDTO();
            }
        }

        public static class FreeReservationsHotelApiHelper
        {
            public const string Resource = "/api/FreeReservationsHotels/";
            
            public static int TestPost(FreeReservationsHotelDTO hotelObj)
            {
                var responseObj = executor.ExecutePost<FreeReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static List<FreeReservationsHotelDTO> TestGet()
            {
                var responseObj = executor.ExecuteGet<List<FreeReservationsHotelDTO>>(BaseUrl, Resource);
                return responseObj;
            }

            public static FreeReservationsHotelDTO TestGet(int id)
            {
                var responseObj = executor.ExecuteGet<FreeReservationsHotelDTO>(BaseUrl, Resource + id.ToString());
                return responseObj;
            }

            public static int TestPut(FreeReservationsHotelDTO hotelObj)
            {
                var responseObj = executor.ExecutePut<FreeReservationsHotelDTO>(hotelObj, BaseUrl, Resource);
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
                        },
                    HotelsAttractions = new List<HotelsAttraction>
                    {
                        new HotelsAttraction
                        {
                            Count = 1, HotelsAttractionType = HotelsAttractionType.SwimmingPool
                        },
                        new HotelsAttraction
                        {
                            Count = 2, HotelsAttractionType = HotelsAttractionType.TennisCourt
                        }
                    },
                };
                return hotel.ToDTO();
            }
        }

        public static class HotelsApiHelper
        {
            public const string Resource = "/api/Hotels/";

            public static int Put(HotelDTO hotel)
            {
                var responseObj = executor.ExecutePut<HotelDTO>(hotel, BaseUrl, string.Format(Resource, hotel.Id));
                return responseObj.Id;
            }

            public static int Post(HotelDTO hotel)
            {
                var responseObj = executor.ExecutePost<HotelDTO>(hotel, BaseUrl, string.Format(Resource, hotel.Id));
                return responseObj.Id;
            }

            public static List<HotelDTO> TestGet()
            {
                var responseObj = executor.ExecuteGet<List<HotelDTO>>(BaseUrl, Resource);
                return responseObj;
            }

            public static HotelDTO TestGet(int id)
            {
                var responseObj = executor.ExecuteGet<HotelDTO>(BaseUrl, string.Format(Resource + "{0}", id));
                return responseObj;
            }

            public static HotelDTO CreateRandomBoundedReservationsHotelDTO()
            {
                return BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO().ToEntity().ToBaseDTO();
            }

            public static HotelDTO CreateRandomFreeReservationsHotelDTO()
            {
                return FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO().ToEntity().ToBaseDTO();
            }
        }

        public static class HotelNumbersApiHelper
        {
            public const string Resource = "/api/Hotels/{0}/HotelNumbers/";

            public static int TestPost(int hotelId, HotelNumberDTO hotelNumber)
            {
                var responseObj = executor.ExecutePost<HotelNumberDTO>(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
                return responseObj.Id;
            }

            public static List<HotelNumberDTO> TestGet(int hotelId)
            {
                var responseObj = executor.ExecuteGet<List<HotelNumberDTO>>(BaseUrl, string.Format(Resource, hotelId));
                return responseObj;
            }

            public static HotelNumberDTO TestGet(int hotelId, int id)
            {
                var responseObj = executor.ExecuteGet<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
                return responseObj;
            }

            public static int TestPut(HotelNumberDTO hotelNumber)
            {
                var responseObj = executor.ExecutePut<HotelNumberDTO>(hotelNumber, BaseUrl, string.Format(Resource, hotelNumber.HotelId));
                return responseObj.Id;
            }

            public static int TestDelete(int hotelId, int id)
            {
                var responseObj = executor.ExecuteDelete<HotelNumberDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
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

            internal static object CreateRandomBoundedReservationsHotelDTO()
            {
                throw new NotImplementedException();
            }

            internal static object CreateRandomFreeReservationsHotelDTO()
            {
                throw new NotImplementedException();
            }
        }

        public static class BoundedPeriodsApiHelper
        {
            private static double counter = 0;
            private static double offset = 7;
            public const string Resource = "/api/BoundedReservations/{0}/Periods/";

            public static int TestPost(int hotelId, BoundedPeriodDTO boundedPeriod)
            {
                var responseObj = executor.ExecutePost<BoundedPeriodDTO>(boundedPeriod, BaseUrl, string.Format(Resource, hotelId));
                
                return responseObj == null ? 0 : responseObj.Id;
            }

            public static List<BoundedPeriodDTO> TestGet(int hotelId)
            {
                var responseObj = executor.ExecuteGet<List<BoundedPeriodDTO>>(BaseUrl, string.Format(Resource, hotelId));
                return responseObj;
            }

            public static BoundedPeriodDTO TestGet(int hotelId, int id)
            {
                var responseObj = executor.ExecuteGet<BoundedPeriodDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
                return responseObj;
            }

            public static int TestPut(BoundedPeriodDTO boundedPeriod)
            {
                var responseObj = executor.ExecutePut<BoundedPeriodDTO>(boundedPeriod, BaseUrl, string.Format(Resource, boundedPeriod.BoundedReservationsHotelId));
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
                var responseObj = executor.ExecutePost(booking, BaseUrl, string.Format(Resource, hotelId), out code);
                return responseObj == null ? 0 : responseObj.Id;
            }

            public static List<FreeBookingDTO> TestGet(int hotelId, out HttpStatusCode code)
            {
                var responseObj = executor.ExecuteGet<List<FreeBookingDTO>>(BaseUrl, string.Format(Resource, hotelId), out code);
                return responseObj;
            }

            public static FreeBookingDTO TestGet(int hotelId, int id, out HttpStatusCode code)
            {
                var responseObj = executor.ExecuteGet<FreeBookingDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id), out code);
                return responseObj;
            }

            public static int TestPut(FreeBookingDTO boundedPeriod, out HttpStatusCode code)
            {
                var responseObj = executor.ExecutePut(boundedPeriod, BaseUrl, string.Format(Resource, boundedPeriod.HotelId), out code);
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

        public static class BoundedBookingApiHelper
        {
            public const string Resource = "api/Booking/BoundedReservations/{0}/";

            public static int TestPost(int hotelId, BoundedBookingDTO booking, out HttpStatusCode code)
            {
                var responseObj = executor.ExecutePost(booking, BaseUrl, string.Format(Resource, hotelId), out code);
                return responseObj == null ? 0 : responseObj.Id;
            }

            public static List<BoundedBookingDTO> TestGet(int hotelId, out HttpStatusCode code)
            {
                var responseObj = executor.ExecuteGet<List<BoundedBookingDTO>>(BaseUrl, string.Format(Resource, hotelId), out code);
                return responseObj;
            }

            public static BoundedBookingDTO TestGet(int hotelId, int id, out HttpStatusCode code)
            {
                var responseObj = executor.ExecuteGet<BoundedBookingDTO>(BaseUrl, string.Format(Resource + "{1}", hotelId, id), out code);
                return responseObj;
            }

            public static int TestPut(BoundedBookingDTO boundedPeriod, out HttpStatusCode code)
            {
                var responseObj = executor.ExecutePut(boundedPeriod, BaseUrl, string.Format(Resource, boundedPeriod.HotelId), out code);
                return responseObj.Id;
            }

            public static BoundedBookingDTO CreateBoundedBookingDTO(int hotelId, int hotelNumberId, int boundedPeriodId)
            {
                var BoundedBookingDTO = new BoundedBookingDTO
                {
                    BoundedPeriodId = boundedPeriodId,
                    Client = new ClientDTO
                    {
                        Name = RandomUtils.RandomString(10),
                        PhoneNumber = RandomUtils.RandomString(10)
                    },
                    HotelId = hotelId,
                    HotelNumberId = hotelNumberId
                };
                return BoundedBookingDTO;
            }
        }
    }
}
