using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Models;

namespace RestTraining.Api.Tests.Controllers
{
    [TestClass]
    public class BoundedPeriodControllerFunctionalTests
    {
        [TestMethod]
        public void Get_Post()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotel();
            var hotelId = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);

            for (int i = 0; i < 5; i++)
            {
                var boundedPeriod = TestHelpers.BoundedPeriodsApiHelper.CreateRandomBoundedPeriod();
                var boundedPeriodId = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotelId, boundedPeriod);

                Assert.AreNotEqual(boundedPeriodId, 0);

                var testBundedPeriod = TestHelpers.BoundedPeriodsApiHelper.TestGet(hotelId, boundedPeriodId);
                Assert.IsNotNull(testBundedPeriod.Id);
                Assert.AreEqual(boundedPeriod.BeginDate, testBundedPeriod.BeginDate);
                Assert.AreEqual(boundedPeriod.EndDate, testBundedPeriod.EndDate);
                Assert.AreEqual(hotelId, testBundedPeriod.BoundedReservationsHotelId);
            }
        }

        [TestMethod]
        public void Post_Intersepted()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotel();
            var hotelId = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);

            var boundedPeriod1 = TestHelpers.BoundedPeriodsApiHelper.CreateRandomBoundedPeriod();
            var boundedPeriod2 = new BoundedPeriod
            {
                BeginDate = boundedPeriod1.BeginDate.AddDays(-3),
                EndDate = DateTime.Today.AddDays(10)
            };
            var boundedPeriod1Id = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotelId, boundedPeriod1);
            var boundedPeriod2Id = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotelId, boundedPeriod2);

            Assert.AreNotEqual(0, boundedPeriod1Id);
            Assert.AreEqual(0, boundedPeriod2Id);
        }

        //[TestMethod]
        //public void Put()
        //{
        //    var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotel();
        //    var hotelId = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);
        //    var hotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId);

        //    for (int i = 0; i < 2; i++)
        //    {
        //        foreach (var hotelNumber in hotelNumbers)
        //        {
        //            var previousIncludedItemsCount = hotelNumber.IncludeItems.Count;
        //            if (previousIncludedItemsCount == 0)
        //                hotelNumber.IncludeItems.Add(new IncludeItem
        //                {
        //                    IncludeItemType = IncludeItemType.Balcony,
        //                    Count = 99
        //                });
        //            else
        //                hotelNumber.IncludeItems.Clear();
        //            var newIncludedItemsCount = hotelNumber.IncludeItems.Count;

        //            TestHelpers.HotelNumbersApiHelper.TestPut(hotelNumber);

        //            var testPutObj = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumber.Id);

        //            Assert.AreEqual(newIncludedItemsCount, testPutObj.IncludeItems.Count);
        //        }


        //        foreach (var hotelNumber in hotelNumbers)
        //        {
        //            var previousWindowViewsCount = hotelNumber.WindowViews.Count;
        //            if (previousWindowViewsCount == 0)
        //                hotelNumber.WindowViews.Add(new WindowView { Type = WindowViewType.Pool });
        //            else
        //                hotelNumber.WindowViews.Clear();
        //            var newWindowViewsCount = hotelNumber.WindowViews.Count;

        //            TestHelpers.HotelNumbersApiHelper.TestPut(hotelNumber);

        //            var testPutObj = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumber.Id);

        //            Assert.AreEqual(newWindowViewsCount, testPutObj.WindowViews.Count);
        //        }
        //    }
        //}
    }
}
