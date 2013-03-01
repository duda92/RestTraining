using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RestTraining.Api.Domain;
using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Api.Domain.Services;
using RestTraining.Api.Infrastructure;
using RestTraining.Api.Tests.Helpers;
using RestTraining.Api.Tests.Utils;
using Rhino.Mocks;

namespace RestTraining.Api.Tests.Repositories
{
    [TestClass]
    public class FreeBookingRepositoryTests
    {
        private IBookingDatesService _mockBookingDatesService;
        private FreeBookingRepository _freeBookingRepository;
        
        DateTime beginDate = DateTime.Today;
        DateTime dateTime = DateTime.Today.AddDays(10);
        private Client _mockClient;

        [TestInitialize()]
        public void SetUp()
        {
            _mockBookingDatesService = MockRepository.GenerateMock<IBookingDatesService>();
            _freeBookingRepository = new FreeBookingRepository(_mockBookingDatesService);

            _mockClient = new Client
                              {
                                  Id = 0,
                                  Name = RandomUtils.RandomString(10),
                                  PhoneNumber = RandomUtils.RandomString(10)
                              };
        }

        [TestMethod]
        public void GivenInvalidHotel_InsertOrUpdate_ThrowNotFoundException()
        {
            var freeBooking = new FreeBooking
                                  {
                                      HotelId = RandomUtils.RandomInt(Int32.MaxValue), // not exists
                                      BeginDate = beginDate,
                                      EndDate = dateTime,
                                      Client = _mockClient
                                  };
            ExceptionAssert.Throws<ParameterNotFoundException>(
                () =>
                    {
                        _freeBookingRepository.InsertOrUpdate(freeBooking);
                    });
        }

        [TestMethod]
        public void GivenInvalidNotFreeHotel_InsertOrUpdate_ThrowNotFoundException()
        {
            var hotel = PersistenceHelper.InsertBoundedReservationsHotel();
            var freeBooking = new FreeBooking
            {
                HotelId = hotel.Id,
                HotelNumberId = RandomUtils.RandomInt(Int32.MaxValue),
                BeginDate = beginDate,
                EndDate = dateTime,
                Client = _mockClient
            };
            ExceptionAssert.Throws<ParameterNotFoundException>(
                () =>
                    {
                        _freeBookingRepository.InsertOrUpdate(freeBooking);
                    });
        }

        [TestMethod]
        public void GivenNotExistedHotelNumber_InsertOrUpdate_ThrowNotFoundException()
        {
            var hotelNumber = PersistenceHelper.InsertHotelNumberForBoundedReservationsHotel();
            var freeBooking = new FreeBooking
            {
                HotelId = hotelNumber.HotelId, 
                HotelNumberId = hotelNumber.Id,
                BeginDate = beginDate,
                EndDate = dateTime,
                Client = _mockClient
            };
            ExceptionAssert.Throws<ParameterNotFoundException>(() =>
                                                                   {
                                                                       _freeBookingRepository.InsertOrUpdate(freeBooking);
                                                                   }); 
        }

        [TestMethod]
        public void GivenInvalidDates_InsertOrUpdate_ThrowInvalidDatesBookingException()
        {
            var hotelNumber = PersistenceHelper.InsertHotelNumberForFreeReservationsHotel();
            var freeBooking = new FreeBooking
            {
                HotelId = hotelNumber.HotelId,
                HotelNumberId = hotelNumber.Id,
                BeginDate = beginDate,
                EndDate = dateTime,
                Client = _mockClient
            };

            _mockBookingDatesService.Stub( x => x.IsFreeBookingValid(Arg<RestTrainingApiContext>.Is.Anything,
                                     Arg<FreeBooking>.Matches(y => y.HotelId == freeBooking.HotelId
                                         && y.HotelNumberId == freeBooking.HotelNumberId
                                         && y.BeginDate == freeBooking.BeginDate
                                         && y.EndDate == freeBooking.EndDate))).Return(false);

            ExceptionAssert.Throws<InvalidDatesBookingException>(() =>
            {
                _freeBookingRepository.InsertOrUpdate(freeBooking);
            });
        }

        [TestCleanup]
        public void Cleanup()
        {
            _freeBookingRepository.Dispose();
        }
    }
}
