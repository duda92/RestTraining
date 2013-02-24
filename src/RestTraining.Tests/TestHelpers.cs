using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using RestTraining.Api.Controllers;
using RestTraining.Api.Models;
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
            
            public static int TestPost(BoundedReservationsHotel hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<BoundedReservationsHotel>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static List<BoundedReservationsHotel> TestGet()
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<BoundedReservationsHotel>>(BaseUrl, Resource);
                return responseObj;
            }

            public static BoundedReservationsHotel TestGet(int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<BoundedReservationsHotel>(BaseUrl, Resource + id.ToString());
                return responseObj;
            }

            public static int TestPut(BoundedReservationsHotel hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<BoundedReservationsHotel>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static BoundedReservationsHotel CreateRandomBoundedReservationsHotel()
            {
                return new BoundedReservationsHotel
                {
                    Title = RandomUtils.RandomString(10),
                    Address = RandomUtils.RandomString(10),
                    HotelNumbers = new List<HotelNumber>
                    {
                        new HotelNumber
                            {
                                HotelNumberType = HotelNumberType.Double,
                                IncludeItems = new List<IncludeItem>
                                    {
                                        new IncludeItem { Count = 1, IncludeItemType = IncludeItemType.AirConditioner },
                                        new IncludeItem { Count = 1, IncludeItemType = IncludeItemType.Balcony }
                                    },
                                    WindowViews = new List<WindowView>
                                        {
                                            new WindowView  { Type = WindowViewType.Pool },
                                            new WindowView  { Type = WindowViewType.Trash },
                                        }
                            }
                    }
                };
            }
        }

        public static class FreeReservationsHotelApiHelper
        {
            public const string Resource = "/api/FreeReservationsHotels/";
            
            public static int TestPost(FreeReservationsHotel hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<FreeReservationsHotel>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static List<FreeReservationsHotel> TestGet()
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<FreeReservationsHotel>>(BaseUrl, Resource);
                return responseObj;
            }

            public static FreeReservationsHotel TestGet(int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<FreeReservationsHotel>(BaseUrl, Resource + id.ToString());
                return responseObj;
            }

            public static int TestPut(FreeReservationsHotel hotelObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<FreeReservationsHotel>(hotelObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static FreeReservationsHotel CreateRandomFreeReservationsHotel()
            {
                return new FreeReservationsHotel
                {
                    Title = RandomUtils.RandomString(10),
                    Address = RandomUtils.RandomString(10),
                    HotelNumbers = new List<HotelNumber>()
                        {
                            new HotelNumber()
                                {
                                    HotelNumberType = RandomUtils.RandomEnumItem<HotelNumberType>(),
                                    IncludeItems = new List<IncludeItem>
                                        {
                                            new IncludeItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(5)  },
                                            new IncludeItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(5), }
                                        },
                                    WindowViews = new List<WindowView> { new WindowView { Type = WindowViewType.Sea }, new WindowView { Type = WindowViewType.Trash }, new WindowView { Type = WindowViewType.Pool }}
                                }
                        }
                };
            }
        }

        public static class ClientsApiHelper
        {
            public const string Resource = "/api/Clients/";
            
            public static int TestPost(Client clientObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<Client>(clientObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static List<Client> TestGet()
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<Client>>(BaseUrl, Resource);
                return responseObj;
            }

            public static Client TestGet(int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<Client>(BaseUrl, (Resource + id.ToString()));
                return responseObj;
            }

            public static int TestPut(Client clientObj)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<Client>(clientObj, BaseUrl, Resource);
                return responseObj.Id;
            }

            public static Client CreateRandomClient()
            {
                return new Client { Name = RandomUtils.RandomString(10), PhoneNumber = RandomUtils.RandomString(10) };
            }
        }

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

            public static int TestPost(int hotelId, HotelNumber hotelNumber)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<HotelNumber>(hotelNumber, BaseUrl, string.Format(Resource, hotelId));
                return responseObj.Id;
            }

            public static List<HotelNumber> TestGet(int hotelId)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<HotelNumber>>(BaseUrl, string.Format(Resource, hotelId));
                return responseObj;
            }

            public static HotelNumber TestGet(int hotelId, int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<HotelNumber>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
                return responseObj;
            }

            public static int TestPut(HotelNumber hotelNumber)
            {
                var responseObj = JsonRequestExecutor.ExecutePut <HotelNumber>(hotelNumber, BaseUrl, string.Format(Resource, hotelNumber.HotelId));
                return responseObj.Id;
            }

            public static HotelNumber CreateRandomHotelNumber()
            {
                return new HotelNumber
                    {
                        HotelNumberType = RandomUtils.RandomEnumItem<HotelNumberType>(),
                        IncludeItems = new List<IncludeItem>
                            {
                                new IncludeItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(10) },
                                new IncludeItem { IncludeItemType = RandomUtils.RandomEnumItem<IncludeItemType>(), Count = RandomUtils.RandomInt(10) },
                            },
                            WindowViews = new List<WindowView>
                                {
                                    new WindowView { Type = RandomUtils.RandomEnumItem<WindowViewType>() },
                                    new WindowView { Type = RandomUtils.RandomEnumItem<WindowViewType>() }
                                }
                    };
            }
        }

        public static class BoundedPeriodsApiHelper
        {
            private static double counter = 0;
            private static double offset = 7;
            public const string Resource = "/api/BoundedReservations/{0}/Periods/";

            public static int TestPost(int hotelId, BoundedPeriod boundedPeriod)
            {
                var responseObj = JsonRequestExecutor.ExecutePost<BoundedPeriod>(boundedPeriod, BaseUrl, string.Format(Resource, hotelId));
                
                return responseObj == null ? 0 : responseObj.Id;
            }

            public static List<BoundedPeriod> TestGet(int hotelId)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<List<BoundedPeriod>>(BaseUrl, string.Format(Resource, hotelId));
                return responseObj;
            }

            public static BoundedPeriod TestGet(int hotelId, int id)
            {
                var responseObj = JsonRequestExecutor.ExecuteGet<BoundedPeriod>(BaseUrl, string.Format(Resource + "{1}", hotelId, id));
                return responseObj;
            }

            public static int TestPut(BoundedPeriod boundedPeriod)
            {
                var responseObj = JsonRequestExecutor.ExecutePut<BoundedPeriod>(boundedPeriod, BaseUrl, string.Format(Resource, boundedPeriod.BoundedReservationsHotelId));
                return responseObj.Id;
            }

            public static BoundedPeriod CreateRandomBoundedPeriod()
            {
                return new BoundedPeriod
                {
                    BeginDate = DateTime.Today.AddDays(counter * offset),
                    EndDate = DateTime.Today.AddDays((++counter) * offset)
                };
            }
        }
    }

    public static class JsonRequestExecutor
    {
        public static T ExecutePut<T>(T obj, string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.PUT);
            var json = JsonConvert.SerializeObject(obj);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObj;
        }

        public static T ExecutePost<T>(T obj, string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.POST);
            var json = JsonConvert.SerializeObject(obj);
            request.AddParameter("text/json", json, ParameterType.RequestBody);
            request.RequestFormat = DataFormat.Json;
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObj;
        }

        public static T ExecuteGet<T>(string baseUrl, string resource)
        {
            var client = new RestClient(baseUrl);
            var request = new RestRequest(resource, Method.GET);
            var response = client.Execute(request);
            var responseObj = JsonConvert.DeserializeObject<T>(response.Content);
            return responseObj;
        }
    }
}
