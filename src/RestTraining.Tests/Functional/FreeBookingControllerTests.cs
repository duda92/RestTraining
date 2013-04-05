using System;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.DTO;
using RestTraining.Api.Tests.Utils;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class FreeBookingControllerTests
    {
        private FreeReservationsHotelDTO hotel;
        private int hotelId;
        private HotelNumberDTO hotelNumber1;
        private HotelNumberDTO hotelNumber2;
        private int hotelNumber1Id;
        private int hotelNumber2Id;

        [TestInitialize]
        public void SetUp()
        {
            hotel = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            hotelId = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotel);
            hotelNumber1 = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumberDTO();
            hotelNumber2 = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumberDTO();
            hotelNumber1Id = TestHelpers.HotelNumbersApiHelper.TestPost(hotelId, hotelNumber1);
            hotelNumber2Id = TestHelpers.HotelNumbersApiHelper.TestPost(hotelId, hotelNumber2);
            hotelNumber1 = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumber1Id);
            hotelNumber2 = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumber2Id);
        }

        [TestMethod]
        public void Post_ExpectCreatedResponse()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(2), DateTime.Today.AddDays(7));
            HttpStatusCode code;
            TestHelpers.FreeBookingApiHelper.TestPost(hotelId, booking1, out code);
            Assert.AreEqual(HttpStatusCode.Created, code);
        }
        
        [TestMethod]
        public void Post_PostIntersectedBookings_ExpectConflictResponseCode1()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(7));
            var booking2 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(99));
            AssretConflictDatesPost(booking1, booking2);
        }

        [TestMethod]
        public void Post_PostIntersectedBookings_ExpectConflictResponseCode2()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(7));
            var booking2 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(7), DateTime.Today.AddDays(99));
            AssretConflictDatesPost(booking1, booking2);
        }

        [TestMethod]
        public void Post_PostIntersectedBookings_ExpectConflictResponseCode3()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(2), DateTime.Today.AddDays(7));
            var booking2 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(2));
            AssretConflictDatesPost(booking1, booking2);
        }

        [TestMethod]
        public void Post_PostIntersectedBookings_ExpectConflictResponseCode4()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
            var booking2 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(5), DateTime.Today.AddDays(7));
            AssretConflictDatesPost(booking1, booking2);
        }

        [TestMethod]
        public void Post_PostIntersectedBookings_ExpectConflictResponseCode5()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
            var booking2 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(-5), DateTime.Today.AddDays(104));
            AssretConflictDatesPost(booking1, booking2);
        }

        [TestMethod]
        public void Post_PostIntersectedBookings_ExpectConflictResponseCode6()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
            var booking2 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(-5), DateTime.Today.AddDays(4));
            AssretConflictDatesPost(booking1, booking2);
        }

        [TestMethod]
        public void Post_PostIntersectedBookings_ExpectConflictResponseCode7()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));
            var booking2 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(98), DateTime.Today.AddDays(102));
            AssretConflictDatesPost(booking1, booking2);
        }

        [TestMethod]
        public void GivenEndDateLessThenBeginDate_Post_ExpectBadRequestCode()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(100), DateTime.Today.AddDays(99));
            AssertBadRequestCode(booking1);
        }

        [TestMethod]
        public void GivenInvalidDate_Post_ExpectBadRequestCode()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today.AddDays(100), default(DateTime));
            AssertBadRequestCode(booking1);
        }

        
        [TestMethod]
        public void Put_PostBookingGetBookingUpdateBookingPutBooking_ExpectOkCodeAndUpdateBookingProperties()
        {
            var booking1 = TestHelpers.FreeBookingApiHelper.CreateFreeBookingDTO(hotelNumber1, DateTime.Today, DateTime.Today.AddDays(100));

            HttpStatusCode code;
            var bookingId = TestHelpers.FreeBookingApiHelper.TestPost(hotelId, booking1, out code);
            Assert.AreEqual(HttpStatusCode.Created, code);

            var postedBooking = TestHelpers.FreeBookingApiHelper.TestGet(hotelId, bookingId, out code);
            DateTime beginDate = DateTime.Today.AddDays(30);
            DateTime endDate = DateTime.Today.AddDays(80);
            postedBooking.BeginDate = beginDate;
            postedBooking.EndDate = endDate;
            string updatedClientName = RandomUtils.RandomString(10);
            string updatedPhoneNumber = RandomUtils.RandomString(10);
            postedBooking.Client.Name = updatedClientName;
            postedBooking.Client.PhoneNumber = updatedPhoneNumber;

            var putBookingId = TestHelpers.FreeBookingApiHelper.TestPut(postedBooking, out code);

            var putBooking = TestHelpers.FreeBookingApiHelper.TestGet(hotelId, putBookingId, out code);

            Assert.AreEqual(HttpStatusCode.OK, code);
            Assert.AreEqual(postedBooking.Id, putBookingId);
            Assert.AreEqual(beginDate, putBooking.BeginDate);
            Assert.AreEqual(endDate, putBooking.EndDate);
            Assert.AreEqual(updatedClientName, putBooking.Client.Name);
            Assert.AreEqual(updatedPhoneNumber, putBooking.Client.PhoneNumber);

        }

        private void AssertBadRequestCode(FreeBookingDTO booking1)
        {
            HttpStatusCode code;
            TestHelpers.FreeBookingApiHelper.TestPost(hotelId, booking1, out code);
            Assert.AreEqual(HttpStatusCode.BadRequest, code);
        }

        private void AssretConflictDatesPost(FreeBookingDTO booking1, FreeBookingDTO booking2)
        {
            HttpStatusCode code;
            TestHelpers.FreeBookingApiHelper.TestPost(hotelId, booking1, out code);
            Assert.AreEqual(HttpStatusCode.Created, code);
            TestHelpers.FreeBookingApiHelper.TestPost(hotelId, booking2, out code);
            Assert.AreEqual(HttpStatusCode.Conflict, code);
        }

        
    }
}
