using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Api.Domain.Services;
using RestTraining.Api.Tests.Helpers;
using RestTraining.Domain;

namespace RestTraining.Api.Tests.Repositories
{
    [TestClass]
    public class HotelsRepositoryTests
    {
        private HotelRepository _hotelRepository;
        private IHotelNumbersUpdateService _hotelNumbersUpdateService;
        private int _testHotelId;


        [TestInitialize()]
        public void SetUp()
        {
            _hotelNumbersUpdateService = new HotelNumbersUpdateService();
            _hotelRepository = new HotelRepository(_hotelNumbersUpdateService);

            var hotel = PersistenceHelper.InsertFreeReservationsHotel();
            _testHotelId = hotel.Id;
            Assert.AreNotEqual(0, _testHotelId);
        }

        [TestMethod]
        public void ClearAllAttractions_InsertOrUpdate_UpdateInstanse()
        {
            var insertedHotel = _hotelRepository.Find(_testHotelId);
            insertedHotel.HotelsAttractions.Clear();
            _hotelRepository.InsertOrUpdate(insertedHotel);
            _hotelRepository.Save();
            var updatedHotel = _hotelRepository.Find(_testHotelId);
            Assert.AreEqual(updatedHotel.HotelsAttractions.Count, 0);
        }

        [TestMethod]
        public void AddAttraction_InsertOrUpdate_UpdateInstanse()
        {
            var insertedHotel = _hotelRepository.Find(_testHotelId);
            insertedHotel.HotelsAttractions.Clear();
            const int insertingAttractionCount = 99;
            const HotelsAttractionType insertingAttractionType = HotelsAttractionType.WaterSlides;
            insertedHotel.HotelsAttractions.Add(new HotelsAttraction
                                                    {
                                                        Count = insertingAttractionCount,
                                                        HotelsAttractionType = insertingAttractionType
                                                    });
            _hotelRepository.InsertOrUpdate(insertedHotel);
            _hotelRepository.Save();
            var updatedHotel = _hotelRepository.Find(_testHotelId);
            Assert.AreEqual(updatedHotel.HotelsAttractions.Count, 1);
            Assert.AreEqual(updatedHotel.HotelsAttractions[0].Count, insertingAttractionCount);
            Assert.AreEqual(updatedHotel.HotelsAttractions[0].HotelsAttractionType, insertingAttractionType);

        }

       

        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
    }
}
