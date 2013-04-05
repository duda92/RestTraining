using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.DTO;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class BoundedBookingControllerTests
    {
        private BoundedReservationsHotelDTO hotel1;
        private int hotel1Id;
        private BoundedReservationsHotelDTO hotel2;
        private int hotel2Id;
        private HotelNumberDTO Hotel1HotelNumber1;
        private HotelNumberDTO Hotel1HotelNumber2;
        private int Hotel1HotelNumber1Id;
        private int Hotel1HotelNumber2Id;

        private int Hotel2HotelNumber1Id;
        private HotelNumberDTO Hotel2HotelNumber1;

        const int YearsAdd = 10;

        [TestInitialize]
        public void SetUp()
        {
            hotel1 = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            hotel1Id = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotel1);
            hotel2 = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            hotel2Id = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotel1);
            Hotel1HotelNumber1 = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumberDTO();
            Hotel1HotelNumber2 = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumberDTO();
            Hotel2HotelNumber1 = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumberDTO();
            Hotel1HotelNumber1Id = TestHelpers.HotelNumbersApiHelper.TestPost(hotel1Id, Hotel1HotelNumber1);
            Hotel1HotelNumber2Id = TestHelpers.HotelNumbersApiHelper.TestPost(hotel1Id, Hotel1HotelNumber2);
            Hotel2HotelNumber1Id = TestHelpers.HotelNumbersApiHelper.TestPost(hotel2Id, Hotel2HotelNumber1);
            
            Hotel1HotelNumber1 = TestHelpers.HotelNumbersApiHelper.TestGet(hotel1Id, Hotel1HotelNumber1Id);
            Hotel1HotelNumber2 = TestHelpers.HotelNumbersApiHelper.TestGet(hotel1Id, Hotel1HotelNumber2Id);
            Hotel2HotelNumber1 = TestHelpers.HotelNumbersApiHelper.TestGet(hotel2Id, Hotel2HotelNumber1Id);
            
        }

        [TestMethod]
        public void Post_ExpectCreatedResponse()
        {
            var boundedPeriod = TestHelpers.BoundedPeriodsApiHelper.CreateRandomBoundedPeriodDTO(DateTime.Now.AddYears(YearsAdd));
            var boundedPeriodId = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotel1Id, boundedPeriod);

            var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotel1Id, Hotel1HotelNumber1Id, boundedPeriodId);
            HttpStatusCode code;
            var postedBookingId = TestHelpers.BoundedBookingApiHelper.TestPost(hotel1Id, booking1, out code);
            Assert.AreEqual(HttpStatusCode.Created, code);
            Assert.AreNotEqual(0, postedBookingId);
        }

        [TestMethod]
        public void Put_PostBoundedBookingGetItUpdateIt_ExpectUpdatedBoundedBooking()
        {
            var boundedPeriod1 = TestHelpers.BoundedPeriodsApiHelper.CreateRandomBoundedPeriodDTO(DateTime.Now.AddYears(YearsAdd).AddYears(YearsAdd));
            var boundedPeriod1Id = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotel1Id, boundedPeriod1);
            
            var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotel1Id, Hotel1HotelNumber1Id, boundedPeriod1Id);
            HttpStatusCode code;
            var postedBookingId = TestHelpers.BoundedBookingApiHelper.TestPost(hotel1Id, booking1, out code);
            var postedBooking = TestHelpers.BoundedBookingApiHelper.TestGet(hotel1Id, postedBookingId, out code);
            Assert.AreEqual(postedBooking.BoundedPeriodId, boundedPeriod1Id);
            
            UpdateBoundedPeriodIdTest(postedBooking, postedBookingId);
            UpdateHotelNumberIdTest(postedBooking, Hotel1HotelNumber2Id, postedBookingId);
            var nameClientForUpdate = "Tosha";
            var phoneNumberForUpdate = "Rybkin";
            UpdateClientTest(postedBooking, nameClientForUpdate, phoneNumberForUpdate, postedBookingId);
        }

        private void UpdateClientTest(BoundedBookingDTO postedBooking, string nameClientForUpdate, string phoneNumberForUpdate, int postedBookingId)
        {
            HttpStatusCode code;
            postedBooking.Client.Name = nameClientForUpdate;
            postedBooking.Client.PhoneNumber = phoneNumberForUpdate;
            TestHelpers.BoundedBookingApiHelper.TestPut(postedBooking, out code);
            var putBooking = TestHelpers.BoundedBookingApiHelper.TestGet(postedBooking.HotelId, postedBookingId, out code);

            Assert.AreEqual(putBooking.HotelNumberId, postedBooking.HotelNumberId);
            Assert.AreEqual(putBooking.HotelId, postedBooking.HotelId);
            Assert.AreEqual(putBooking.Client.Name, nameClientForUpdate);
            Assert.AreEqual(putBooking.Client.PhoneNumber, phoneNumberForUpdate);
            Assert.AreEqual(putBooking.HotelId, postedBooking.HotelId);

            Assert.AreEqual(putBooking.Id, postedBooking.Id);
        }

        private void UpdateHotelNumberIdTest(BoundedBookingDTO postedBooking, int hotelNumberForUpdateId, int postedBookingId)
        {
            HttpStatusCode code;
            postedBooking.HotelNumberId = hotelNumberForUpdateId;
            TestHelpers.BoundedBookingApiHelper.TestPut(postedBooking, out code);
            var putBooking = TestHelpers.BoundedBookingApiHelper.TestGet(postedBooking.HotelId, postedBookingId, out code);

            Assert.AreEqual(putBooking.HotelNumberId, hotelNumberForUpdateId);
            Assert.AreEqual(putBooking.HotelId, postedBooking.HotelId);
            Assert.AreEqual(putBooking.Client.Name, postedBooking.Client.Name);
            Assert.AreEqual(putBooking.Client.PhoneNumber, postedBooking.Client.PhoneNumber);
            Assert.AreEqual(putBooking.BoundedPeriodId, postedBooking.BoundedPeriodId);
            Assert.AreEqual(putBooking.Id, postedBooking.Id);
        }
  
        private void UpdateBoundedPeriodIdTest(BoundedBookingDTO postedBooking, int postedBookingId)
        {
            var boundedPeriodForUpdate = TestHelpers.BoundedPeriodsApiHelper.CreateRandomBoundedPeriodDTO(DateTime.Now.AddYears(YearsAdd).AddYears(YearsAdd).AddYears(YearsAdd));
            var boundedPeriodForUpdateId = TestHelpers.BoundedPeriodsApiHelper.TestPost(hotel1Id, boundedPeriodForUpdate);

            HttpStatusCode code;
            postedBooking.BoundedPeriodId = boundedPeriodForUpdateId;
            TestHelpers.BoundedBookingApiHelper.TestPut(postedBooking, out code);
            var putBooking = TestHelpers.BoundedBookingApiHelper.TestGet(postedBooking.HotelId, postedBookingId, out code);

            Assert.AreEqual(putBooking.BoundedPeriodId, boundedPeriodForUpdateId);
            Assert.AreEqual(putBooking.HotelId, postedBooking.HotelId);
            Assert.AreEqual(putBooking.Client.Name, postedBooking.Client.Name);
            Assert.AreEqual(putBooking.Client.PhoneNumber, postedBooking.Client.PhoneNumber);
            Assert.AreEqual(putBooking.HotelNumberId, postedBooking.HotelNumberId);
            Assert.AreEqual(putBooking.Id, postedBooking.Id);
        }

        //[TestMethod]
        //public void Post_PostIntersectedBookings_ExpectConflictResponseCode1()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(7));
        //    var booking2 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(99));
        //    AssretConflictDatesPost(booking1, booking2);
        //}

        //[TestMethod]
        //public void Post_PostIntersectedBookings_ExpectConflictResponseCode2()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(7));
        //    var booking2 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(7), DateTime.Today.AddDays(99));
        //    AssretConflictDatesPost(booking1, booking2);
        //}

        //[TestMethod]
        //public void Post_PostIntersectedBookings_ExpectConflictResponseCode3()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(2), DateTime.Today.AddDays(7));
        //    var booking2 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(2));
        //    AssretConflictDatesPost(booking1, booking2);
        //}

        //[TestMethod]
        //public void Post_PostIntersectedBookings_ExpectConflictResponseCode4()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
        //    var booking2 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(5), DateTime.Today.AddDays(7));
        //    AssretConflictDatesPost(booking1, booking2);
        //}

        //[TestMethod]
        //public void Post_PostIntersectedBookings_ExpectConflictResponseCode5()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
        //    var booking2 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(-5), DateTime.Today.AddDays(104));
        //    AssretConflictDatesPost(booking1, booking2);
        //}

        //[TestMethod]
        //public void Post_PostIntersectedBookings_ExpectConflictResponseCode6()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
        //    var booking2 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(-5), DateTime.Today.AddDays(4));
        //    AssretConflictDatesPost(booking1, booking2);
        //}

        //[TestMethod]
        //public void Post_PostIntersectedBookings_ExpectConflictResponseCode7()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
        //    var booking2 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(98), DateTime.Today.AddDays(102));
        //    AssretConflictDatesPost(booking1, booking2);
        //}

        //[TestMethod]
        //public void GivenEndDateLessThenBeginDate_Post_ExpectBadRequestCode()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(100), DateTime.Today.AddDays(99));
        //    AssertBadRequestCode(booking1);
        //}

        //[TestMethod]
        //public void GivenInvalidDate_Post_ExpectBadRequestCode()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today.AddDays(100), default(DateTime));
        //    AssertBadRequestCode(booking1);
        //}


        //[TestMethod]
        //public void Put_PostBookingGetBookingUpdateBookingPutBooking_ExpectOkCodeAndUpdateBookingProperties()
        //{
        //    var booking1 = TestHelpers.BoundedBookingApiHelper.CreateBoundedBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));

        //    HttpStatusCode code;
        //    var bookingId = TestHelpers.BoundedBookingApiHelper.TestPost(hotelId, booking1, out code);
        //    Assert.AreEqual(HttpStatusCode.Created, code);

        //    var postedBooking = TestHelpers.BoundedBookingApiHelper.TestGet(hotelId, bookingId, out code);
        //    DateTime beginDate = DateTime.Today.AddDays(30);
        //    DateTime endDate = DateTime.Today.AddDays(80);
        //    postedBooking.BeginDate = beginDate;
        //    postedBooking.EndDate = endDate;
        //    string updatedClientName = RandomUtils.RandomString(10);
        //    string updatedPhoneNumber = RandomUtils.RandomString(10);
        //    postedBooking.Client.Name = updatedClientName;
        //    postedBooking.Client.PhoneNumber = updatedPhoneNumber;

        //    var putBookingId = TestHelpers.BoundedBookingApiHelper.TestPut(postedBooking, out code);

        //    var putBooking = TestHelpers.BoundedBookingApiHelper.TestGet(hotelId, putBookingId, out code);

        //    Assert.AreEqual(HttpStatusCode.OK, code);
        //    Assert.AreEqual(postedBooking.Id, putBookingId);
        //    Assert.AreEqual(beginDate, putBooking.BeginDate);
        //    Assert.AreEqual(endDate, putBooking.EndDate);
        //    Assert.AreEqual(updatedClientName, putBooking.Client.Name);
        //    Assert.AreEqual(updatedPhoneNumber, putBooking.Client.PhoneNumber);

        //}

        //private void AssertBadRequestCode(BoundedBookingDTO booking1)
        //{
        //    HttpStatusCode code;
        //    TestHelpers.BoundedBookingApiHelper.TestPost(hotelId, booking1, out code);
        //    Assert.AreEqual(HttpStatusCode.BadRequest, code);
        //}

        //private void AssretConflictDatesPost(BoundedBookingDTO booking1, BoundedBookingDTO booking2)
        //{
        //    HttpStatusCode code;
        //    TestHelpers.BoundedBookingApiHelper.TestPost(hotelId, booking1, out code);
        //    Assert.AreEqual(HttpStatusCode.Created, code);
        //    TestHelpers.BoundedBookingApiHelper.TestPost(hotelId, booking2, out code);
        //    Assert.AreEqual(HttpStatusCode.Conflict, code);
        //}

    }
}
