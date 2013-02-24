using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Domain;

namespace RestTraining.Api.Tests.Controllers
{
    [TestClass]
    public class HotelNumbersControllerFunctionalTests
    {
        [TestMethod]
        public void Get()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotel();
            var hotelId = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);

            for (int i = 0; i < 5; i++)
            {
                var hotelNumber = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumber();
                var hotelNumberId = TestHelpers.HotelNumbersApiHelper.TestPost(hotelId, hotelNumber);

                Assert.AreNotEqual(hotelNumberId, 0);

                var testHotelNumber = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumberId);
                Assert.AreEqual(hotelNumberId, testHotelNumber.Id);
                Assert.AreEqual(hotelId, testHotelNumber.HotelId);
            }
        }

        [TestMethod]
        public void Put()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotel();
            var hotelId = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);
            var hotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId);

            for (int i = 0; i < 2; i++)
            {
                foreach (var hotelNumber in hotelNumbers)
                {
                    var previousIncludedItemsCount = hotelNumber.IncludeItems.Count;
                    if (previousIncludedItemsCount == 0)
                        hotelNumber.IncludeItems.Add(new IncludeItem
                            {
                                IncludeItemType = IncludeItemType.Balcony,
                                Count = 99
                            });
                    else
                        hotelNumber.IncludeItems.Clear();
                    var newIncludedItemsCount = hotelNumber.IncludeItems.Count;

                    TestHelpers.HotelNumbersApiHelper.TestPut(hotelNumber);

                    var testPutObj = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumber.Id);

                    Assert.AreEqual(newIncludedItemsCount, testPutObj.IncludeItems.Count);
                }


                foreach (var hotelNumber in hotelNumbers)
                {
                    var previousWindowViewsCount = hotelNumber.WindowViews.Count;
                    if (previousWindowViewsCount == 0)
                        hotelNumber.WindowViews.Add(new WindowView { Type = WindowViewType.Pool });
                    else
                        hotelNumber.WindowViews.Clear();
                    var newWindowViewsCount = hotelNumber.WindowViews.Count;

                    TestHelpers.HotelNumbersApiHelper.TestPut(hotelNumber);

                    var testPutObj = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumber.Id);

                    Assert.AreEqual(newWindowViewsCount, testPutObj.WindowViews.Count);
                }
            }
        }
    }
}
