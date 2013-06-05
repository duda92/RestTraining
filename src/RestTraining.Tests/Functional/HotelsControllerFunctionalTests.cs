using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Common.DTO;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class HotelsControllerTests
    {
        [TestMethod]
        public void Post_Get_PostHotelsGetHotels_ExpectGetPostedHotels()
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
        public void Post_Get_Put_PostHotelGetHotelPutHotel_ExpectHotelMatchesUpdatedProperies()
        {
            var hotelObj = TestHelpers.HotelsApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotelId = TestHelpers.HotelsApiHelper.Post(hotelObj);
            hotelObj = TestHelpers.HotelsApiHelper.TestGet(hotelId);

            for (int i = 0; i < 2; i++)
            {
                var previousHotelsAttractionCount = hotelObj.HotelsAttractions.Count;
                if (previousHotelsAttractionCount == 0)
                    hotelObj.HotelsAttractions.Add(new HotelsAttraction
                    {
                        HotelsAttractionType = HotelsAttractionType.SwimmingPool,
                        Count = 99
                    }.ToDTO());
                else
                    hotelObj.HotelsAttractions.Clear();
                var newHotelsAttractionsCount = hotelObj.HotelsAttractions.Count;

                TestHelpers.HotelsApiHelper.Put(hotelObj);

                var testPutObj = TestHelpers.HotelsApiHelper.TestGet(hotelId);
                Assert.AreEqual(newHotelsAttractionsCount, testPutObj.HotelsAttractions.Count);
            }
        }

        [TestMethod]
        public void Put_Get_PostHotelsGetHotelsPutHotels_ExpectHotelsUpdated()
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
        public void Put_PostHotelsGetHotelsChangeHotelType_ExpectHotelNumbersWithoutChanges()
        {
            var hotel = TestHelpers.BoundedReservationsHotelApiHelper.CreateRandomBoundedReservationsHotelDTO();
            var hotelId = TestHelpers.BoundedReservationsHotelApiHelper.TestPost(hotel);

            var insertedHotel = TestHelpers.HotelsApiHelper.TestGet(hotelId);
            insertedHotel.Type = HotelDTO.TypeDescriminator.Free;

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
        public void Post_Put_PostHotelsGetHotelsChangeHotelType_ExpectHotelIdNotChanged()
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
    }
}
