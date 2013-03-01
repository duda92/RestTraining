using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
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
    public class HotelNumbersRepositoryTests
    {
        private HotelNumbersRepository _hotelNumberRepository;
        private IHotelNumbersUpdateService _hotelNumbersUpdateService;
        private int _testHotelId;


        [TestInitialize()]
        public void SetUp()
        {
            _hotelNumbersUpdateService = new HotelNumbersUpdateService();
            _hotelNumberRepository = new HotelNumbersRepository(_hotelNumbersUpdateService);

            var hotel = PersistenceHelper.InsertFreeReservationsHotel();
            _testHotelId = hotel.Id;
        }

        [TestMethod]
        public void InsertOrUpdate_UpdateInstanse()
        {
            var hotelNumber = new HotelNumber
                                  {
                                      Id = 0,
                                      HotelId = _testHotelId,
                                      HotelNumberType = HotelNumberType.Double
                                  };
            hotelNumber.WindowViews.Add(new WindowView { Type = WindowViewType.Pool });
            hotelNumber.IncludeItems.Add(new IncludedItem { IncludeItemType = IncludeItemType.AirConditioner, Count = 1 });

            _hotelNumberRepository.InsertOrUpdate(hotelNumber);
            _hotelNumberRepository.Save();

            var hotelNumberId = hotelNumber.Id;
            Assert.AreNotEqual(0, hotelNumberId);

            var insertedHotel = _hotelNumberRepository.Find(hotelNumberId);
            insertedHotel.HotelNumberType = HotelNumberType.Single;
            _hotelNumberRepository.InsertOrUpdate(hotelNumber);
            _hotelNumberRepository.Save();
            var updatedHotel = _hotelNumberRepository.Find(hotelNumberId);
            Assert.AreEqual(updatedHotel.HotelNumberType, HotelNumberType.Single);
        }

        [TestMethod]
        public void InsertOrUpdate_UpdateIncludedItems()
        {
            var hotelNumber = new HotelNumber
            {
                Id = 0,
                HotelId = _testHotelId,
                HotelNumberType = HotelNumberType.Double
            };
            hotelNumber.IncludeItems.Add(new IncludedItem { IncludeItemType = IncludeItemType.AirConditioner, Count = 1 });
            hotelNumber.IncludeItems.Add(new IncludedItem { IncludeItemType = IncludeItemType.Balcony, Count = 1 });

            _hotelNumberRepository.InsertOrUpdate(hotelNumber);
            _hotelNumberRepository.Save();

            var hotelNumberId = hotelNumber.Id;
            Assert.AreNotEqual(0, hotelNumberId);

            var insertedHotelNumber = _hotelNumberRepository.Find(hotelNumberId);
            var workingInsertedHotelNumber = new HotelNumber
                                                 {
                                                     Id = insertedHotelNumber.Id,
                                                     HotelId = insertedHotelNumber.HotelId,
                                                     HotelNumberType = insertedHotelNumber.HotelNumberType,
                                                     IncludeItems = new List<IncludedItem>(insertedHotelNumber.IncludeItems.Select(x =>
                                                         new IncludedItem
                                                             {
                                                                 Id = x.Id,
                                                                 Count = x.Count,
                                                                 HotelNumberId = x.HotelNumberId,
                                                                 IncludeItemType = x.IncludeItemType
                                                             })),
                                                     WindowViews = new List<WindowView>(insertedHotelNumber.WindowViews.Select(x => new WindowView { Type = x.Type }))
                                                 };
            

            const int newCount = 912;
            workingInsertedHotelNumber.IncludeItems.Remove(workingInsertedHotelNumber.IncludeItems.First(x => x.IncludeItemType == IncludeItemType.AirConditioner));
            workingInsertedHotelNumber.IncludeItems.Add(new IncludedItem { IncludeItemType = IncludeItemType.TvSet, Count = newCount });
            _hotelNumberRepository.InsertOrUpdate(workingInsertedHotelNumber);
            _hotelNumberRepository.Save();
            var updatedHotel = _hotelNumberRepository.Find(hotelNumberId);
            Assert.AreEqual(2, updatedHotel.IncludeItems.Count);
            Assert.IsTrue(updatedHotel.IncludeItems.Any(x => x.IncludeItemType == IncludeItemType.Balcony));
            Assert.IsTrue(updatedHotel.IncludeItems.Any(x => x.IncludeItemType == IncludeItemType.TvSet));
        }

        [TestMethod]
        public void InsertOrUpdate_UpdateWindowsViews()
        {
            var hotelNumber = new HotelNumber
            {
                Id = 0,
                HotelId = _testHotelId,
                HotelNumberType = HotelNumberType.Double
            };
            hotelNumber.WindowViews.Add(new WindowView { Type = WindowViewType.Pool });
            hotelNumber.WindowViews.Add(new WindowView { Type = WindowViewType.Sea });
            
            _hotelNumberRepository.InsertOrUpdate(hotelNumber);
            _hotelNumberRepository.Save();

            var hotelNumberId = hotelNumber.Id;
            Assert.AreNotEqual(0, hotelNumberId);

            var insertedHotelNumber = _hotelNumberRepository.Find(hotelNumberId);
            var workingInsertedHotelNumber = new HotelNumber
            {
                Id = insertedHotelNumber.Id,
                HotelId = insertedHotelNumber.HotelId,
                HotelNumberType = insertedHotelNumber.HotelNumberType,
                IncludeItems = new List<IncludedItem>(insertedHotelNumber.IncludeItems.Select(x =>
                    new IncludedItem
                    {
                        Id = x.Id,
                        Count = x.Count,
                        HotelNumberId = x.HotelNumberId,
                        IncludeItemType = x.IncludeItemType
                    })),
                WindowViews = new List<WindowView>(insertedHotelNumber.WindowViews.Select(x => new WindowView { Type = x.Type }))
            };

            workingInsertedHotelNumber.WindowViews.Add(new WindowView { Type = WindowViewType.Trash });
            workingInsertedHotelNumber.WindowViews.RemoveAll(x => x.Type == WindowViewType.Sea );

            _hotelNumberRepository.InsertOrUpdate(workingInsertedHotelNumber);
            _hotelNumberRepository.Save();
            var updatedHotel = _hotelNumberRepository.Find(hotelNumberId);
            Assert.AreEqual(updatedHotel.WindowViews.Count, 2);

            Assert.IsTrue(updatedHotel.WindowViews.Any(x => x.Type == WindowViewType.Pool));
            Assert.IsTrue(updatedHotel.WindowViews.Any(x => x.Type == WindowViewType.Trash));
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
