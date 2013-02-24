using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Tests.Utils;
using RestTraining.Domain;

namespace RestTraining.Api.Tests.Controllers
{
    [TestClass]
    public class FreeReservationsHotelsControllerFunctionalTests
    {
        [TestMethod]
        public void Get()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotel();
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);
            var all = TestHelpers.FreeReservationsHotelApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Title == hotelObj.Title && x.Address == hotelObj.Address));
        }

        [TestMethod]
        public void Get_Id()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotel();
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);

            List<FreeReservationsHotel> all = TestHelpers.FreeReservationsHotelApiHelper.TestGet();
            foreach (var client1 in all)
            {
                var result = TestHelpers.FreeReservationsHotelApiHelper.TestGet(client1.Id);
                Assert.IsNotNull(result.Address);
                Assert.AreEqual(client1.Address, result.Address);
                Assert.AreEqual(client1.Id, result.Id);
                Assert.AreEqual(client1.Title, result.Title);
            }
        }

        [TestMethod]
        public void Post()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotel();
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);
            var all = TestHelpers.FreeReservationsHotelApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Title == hotelObj.Title && x.Address == hotelObj.Address));
        }

        [TestMethod]
        public void Put()
        {
            TestHelpers.FreeReservationsHotelApiHelper.TestPost(TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotel());
            List<FreeReservationsHotel> all = TestHelpers.FreeReservationsHotelApiHelper.TestGet();

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
