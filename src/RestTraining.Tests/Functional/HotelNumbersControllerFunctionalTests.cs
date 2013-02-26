using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.DTO;
using RestTraining.Api.Domain.Entities;
using RestTraining.Domain;

namespace RestTraining.Api.Tests.Functional
{
    [TestClass]
    public class HotelNumbersControllerTests
    {
        [TestMethod]
        public void Get()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            var hotelId = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);

            for (int i = 0; i < 5; i++)
            {
                var hotelNumber = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumberDTO();
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
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            var hotelId = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);
            var hotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId);

            for (int i = 0; i < 2; i++)
            {
                foreach (var hotelNumber in hotelNumbers)
                {
                    var previousIncludedItemsCount = hotelNumber.IncludeItems.Count;
                    if (previousIncludedItemsCount == 0)
                        hotelNumber.IncludeItems.Add(new IncludedItem
                            {
                                IncludeItemType = IncludeItemType.Balcony,
                                Count = 99
                            }.ToDTO());
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
                        hotelNumber.WindowViews.Add(WindowViewType.Pool);
                    else
                        hotelNumber.WindowViews.Clear();
                    var newWindowViewsCount = hotelNumber.WindowViews.Count;

                    TestHelpers.HotelNumbersApiHelper.TestPut(hotelNumber);

                    var testPutObj = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumber.Id);

                    Assert.AreEqual(newWindowViewsCount, testPutObj.WindowViews.Count);
                }
            }
        }

        [TestMethod]
        public void Delete()
        {
            var hotelObj = TestHelpers.FreeReservationsHotelApiHelper.CreateRandomFreeReservationsHotelDTO();
            var hotelId = TestHelpers.FreeReservationsHotelApiHelper.TestPost(hotelObj);
            var hotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId);

            var hotelNumber = TestHelpers.HotelNumbersApiHelper.CreateRandomHotelNumberDTO();
            var hotelNumberId = TestHelpers.HotelNumbersApiHelper.TestPost(hotelId, hotelNumber);

            Assert.AreNotEqual(hotelNumberId, 0);
            
            for (int i = 0; i < hotelNumbers.Count; i++)
            {
                var testHotelNumber = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId, hotelNumberId);
                Assert.AreNotEqual(0, testHotelNumber.Id);

                var testAllHotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId);
                Assert.IsTrue(testAllHotelNumbers.Any(x => x.Id == testHotelNumber.Id));

                TestHelpers.HotelNumbersApiHelper.TestDelete(hotelId, hotelNumberId);
                hotelNumbers = TestHelpers.HotelNumbersApiHelper.TestGet(hotelId);
                Assert.IsFalse(hotelNumbers.Any(x => x.Id == hotelNumberId));
            }
            
        }

    }
}
