using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Common.DTO;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class BoundedPeriodControllerTests
    {
        [TestMethod]
        public void Post_Get_PostNotIntersepterBoundedPeriods_ExpectGetPostedBoundedPeriods()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotelId = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);

            var lastEndDate = DateTime.Today;

            for (int i = 0; i < 5; i++)
            {
                var boundedPeriod = TestHelpers.BoundedPeriodsApiHelper.CreateRandomBoundedPeriodDTO(lastEndDate.AddDays(1));
                var boundedPeriodId = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotelId, boundedPeriod);

                Assert.AreNotEqual(boundedPeriodId, 0);
                lastEndDate = boundedPeriod.EndDate;

                var testBundedPeriod = TestHelpers.BoundedPeriodsApiHelper.TestGet(hotelId, boundedPeriodId);
                Assert.IsNotNull(testBundedPeriod.Id);
                Assert.AreEqual(boundedPeriod.BeginDate, testBundedPeriod.BeginDate);
                Assert.AreEqual(boundedPeriod.EndDate, testBundedPeriod.EndDate);
                Assert.AreEqual(hotelId, testBundedPeriod.BoundedReservationsHotelId);
            }
        }

        [TestMethod]
        public void Post_Get_PostInterseptedBoundedPeriods_ExpectFirstPostedAndInterseptedNot()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotelId = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);
            
            var boundedPeriod1 = TestHelpers.BoundedPeriodsApiHelper.CreateRandomBoundedPeriodDTO(DateTime.Today);
            var boundedPeriod2 = new BoundedPeriodDTO
            {
                BeginDate = boundedPeriod1.BeginDate.AddDays(-3),
                EndDate = DateTime.Today.AddDays(10)
            };
            var boundedPeriod1Id = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotelId, boundedPeriod1);
            var boundedPeriod2Id = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotelId, boundedPeriod2);

            Assert.AreNotEqual(0, boundedPeriod1Id);
            Assert.AreEqual(0, boundedPeriod2Id);
        }
    }
}
