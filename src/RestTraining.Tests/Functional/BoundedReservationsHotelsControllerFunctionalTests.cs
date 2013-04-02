using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Tests.Utils;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class BoundedReservationsHotelsControllerTests
    {
        private const string Resource = TestHelpers.BoundedReservationsHotelApiHelper.Resource;
        private const string BaseUrl = TestHelpers.BaseUrl;

        [TestMethod]
        public void Post_Get_PostHotel_ExpectGetPostedHotel()
        {
            var hotelObj = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotelObj);
            var all = TestHelpers.BoundedReservationsHotelApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Title == hotelObj.Title && x.Address == hotelObj.Address));
        }

        [TestMethod]
        public void GetAll_GetById_PostHotelGetAllGetForEachById_ExpectGetAllMatchesForeachGetById()
        {
            var postHotel = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(postHotel);

            var getHotels = TestHelpers.BoundedReservationsHotelApiHelper.TestGet();
            foreach (var hotels in getHotels)
            {
                var result = TestHelpers.BoundedReservationsHotelApiHelper.TestGet(hotels.Id);
                Assert.IsNotNull(result.Address);
                Assert.AreEqual(hotels.Address, result.Address);
                Assert.AreEqual(hotels.Id, result.Id);
                Assert.AreEqual(hotels.Title, result.Title);
            }
        }

        [TestMethod]
        public void Put_Post_GetById_PostHotelGetHotelChangeHotelPutHotelGetHotel_ExpectHotelAfterPutMatchesUpdatedProperties()
        {
            TestHelpers.BoundedReservationsHotelApiHelper.TestPost(TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO());
            var all = TestHelpers.BoundedReservationsHotelApiHelper.TestGet();

            var hotelObj = all.FirstOrDefault();
            hotelObj.Title = RandomUtils.RandomString(10);
            var id = hotelObj.Id;

            TestHelpers.BoundedReservationsHotelApiHelper.TestPut(hotelObj);
            var test = TestHelpers.BoundedReservationsHotelApiHelper.TestGet(hotelObj.Id);
            Assert.AreEqual(hotelObj.Title, test.Title);
            Assert.AreEqual(hotelObj.Address, test.Address);
            Assert.AreEqual(hotelObj.Id, id);
        }

       
    }
}
