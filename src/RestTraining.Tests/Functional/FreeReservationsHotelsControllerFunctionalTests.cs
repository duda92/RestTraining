using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Tests.Utils;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class FreeReservationsHotelsControllerTests
    {
        [TestMethod]
        public void Post_Get_PostHotel_ExpectGetPostedHotel()
        {
            var hotel = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotel);
            var all = TestHelpers.FreeReservationsHotelApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Title == hotel.Title && x.Address == hotel.Address));
        }

        [TestMethod]
        public void GetAll_GetById_PostHotelGetAllGetForEachById_ExpectGetAllMatchesForeachGetById()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);

            var getHotels = TestHelpers.FreeReservationsHotelApiHelper.TestGet();
            foreach (var hotel in getHotels)
            {
                var result = TestHelpers.FreeReservationsHotelApiHelper.TestGet(hotel.Id);
                Assert.IsNotNull(result.Address);
                Assert.AreEqual(hotel.Address, result.Address);
                Assert.AreEqual(hotel.Id, result.Id);
                Assert.AreEqual(hotel.Title, result.Title);
            }
        }

        [TestMethod]
        public void Put_Post_GetById_PostHotelGetHotelChangeHotelPutHotelGetHotel_ExpectHotelAfterPutMatchesUpdatedProperties()
        {
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO());
            var all = TestHelpers.FreeReservationsHotelApiHelper.TestGet();

            var hotelObj = all.FirstOrDefault();
            hotelObj.Title = RandomUtils.RandomString(10);
            var id = hotelObj.Id;

            TestHelpers.FreeReservationsHotelApiHelper.TestPut(hotelObj);
            var test = TestHelpers.FreeReservationsHotelApiHelper.TestGet(hotelObj.Id);
            Assert.AreEqual(hotelObj.Title, test.Title);
            Assert.AreEqual(hotelObj.Address, test.Address);
            Assert.AreEqual(hotelObj.Id, id);
        }

    }
}
