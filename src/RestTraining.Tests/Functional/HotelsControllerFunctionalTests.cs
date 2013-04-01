using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.DTO;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class HotelsControllerTests
    {
        [TestMethod]
        public void Post_Get_ReturnsPostedHotels()
        {
            var hotel1 = TestHelpers.HotelsApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotel2 = TestHelpers.HotelsApiHelper.CreateRandomFreeReservationsHotelDTO();
            TestHelpers.HotelsApiHelper.Post(hotel2);
            TestHelpers.HotelsApiHelper.Post(hotel1);
            var all = TestHelpers.HotelsApiHelper.TestGet();
            Assert.IsTrue(all.Any(x => x.Address == hotel1.Address && x.Title == hotel1.Title && x.Type == HotelDTO.TypeDescriminator.Bounded));
            Assert.IsTrue(all.Any(x => x.Address == hotel2.Address && x.Title == hotel2.Title && x.Type == HotelDTO.TypeDescriminator.Free));
        }

        [TestMethod]
        public void Put_Get_ExpectSetAllTheFieldsUpdates()
        {
            var hotel1 = TestHelpers.HotelsApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotel2 = TestHelpers.HotelsApiHelper.CreateRandomFreeReservationsHotelDTO();
            var hotel1Id = TestHelpers.HotelsApiHelper.Post(hotel1);
            var hotel2Id = TestHelpers.HotelsApiHelper.Post(hotel2);

            const string newTitle1 = "zzz";
            const string newAddress1 = "zzz";
            const string newTitle2 = "zzz";
            const string newAddress2 = "zzz";

            hotel1 = TestHelpers.HotelsApiHelper.TestGet(hotel1Id);
            hotel2 = TestHelpers.HotelsApiHelper.TestGet(hotel2Id);

            hotel1.Title = newTitle1;
            hotel1.Address = newAddress1;
            hotel2.Title = newTitle2;
            hotel2.Address = newAddress2;
            hotel1.Type = HotelDTO.TypeDescriminator.Free;
            hotel2.Type = HotelDTO.TypeDescriminator.Bounded;

            TestHelpers.HotelsApiHelper.Put(hotel2);
            TestHelpers.HotelsApiHelper.Put(hotel1);

            hotel1 = TestHelpers.HotelsApiHelper.TestGet(hotel1Id);
            hotel2 = TestHelpers.HotelsApiHelper.TestGet(hotel2Id);

            Assert.AreEqual(hotel1.Address, newAddress1);
            Assert.AreEqual(hotel1.Title, newTitle1);
            Assert.AreEqual(hotel2.Address, newAddress2);
            Assert.AreEqual(hotel2.Title, newTitle2);

            Assert.AreEqual(hotel1.Type, HotelDTO.TypeDescriminator.Free);
            Assert.AreEqual(hotel2.Type, HotelDTO.TypeDescriminator.Bounded);
        }

        [TestMethod]
        public void Put_ReturnsHotelNumbersWithoutChanges()
        {
            var hotel = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotelId = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotel);

            var insertedHotel = TestHelpers.HotelsApiHelper.TestGet(hotelId);
            insertedHotel.Type = DTO.HotelDTO.TypeDescriminator.Free;

            var insertedHotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId);
            var updatedHotelId = TestHelpers.HotelsApiHelper.Put(insertedHotel);

            var updatedHotel = TestHelpers.HotelsApiHelper.TestGet(updatedHotelId);
            var getUpdatedHotelFromFreeHotelsController = TestHelpers.FreeReservationsHotelApiHelper.TestGet(updatedHotel.Id);

            var updatedHotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(updatedHotel.Id);

            Assert.AreEqual(insertedHotelNumbers.Count, updatedHotelNumbers.Count);
            for (int i = 0; i < getUpdatedHotelFromFreeHotelsController.HotelNumbers.Count; i++)
            {
                Assert.AreEqual(insertedHotelNumbers[i].Id, updatedHotelNumbers[i].Id);
            }
        }

        [TestMethod]
        public void PostAndPut_ReturnsIdNotChanged()
        {
            var hotel = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotelId = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotel);
            
            Assert.AreNotEqual(0, hotelId);
            
            var insertedHotel = TestHelpers.HotelsApiHelper.TestGet(hotelId);
            Assert.AreNotEqual(0, insertedHotel.Id);
            Assert.AreEqual(hotelId, insertedHotel.Id);
            
            var updatedHotelId = TestHelpers.HotelsApiHelper.Put(insertedHotel);
            Assert.AreEqual(hotelId, updatedHotelId);
            
            var updatedHotel = TestHelpers.HotelsApiHelper.TestGet(updatedHotelId);
            Assert.AreEqual(hotelId, updatedHotel.Id);
        }

        [TestMethod]
        public void Post_IdNotNull()
        {
            var hotel1 = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotel1Id = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotel1);
           
            var hotel2 = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            var hotel2Id = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotel2);

            Assert.AreNotEqual(0, hotel1Id);
            Assert.AreNotEqual(0, hotel2Id);
        }
    }
}
