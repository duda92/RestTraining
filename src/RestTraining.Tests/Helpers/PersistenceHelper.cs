using RestTraining.Api.Domain.Entities;
using RestTraining.Api.Domain.Repositories;
using RestTraining.Api.Domain.Services;
using RestTraining.Domain;

namespace RestTraining.Api.Tests.Helpers
{
    public static class PersistenceHelper
    {
        private static HotelNumbersUpdateService _hotelNumbersUpdateService;
        
        public static FreeReservationsHotel InsertFreeReservationsHotel()
        {
            _hotelNumbersUpdateService = new HotelNumbersUpdateService();
            using (var hotelRepository = new HotelRepository(_hotelNumbersUpdateService))
            {
                var hotel = new FreeReservationsHotel
                {
                    Address = "test",
                    Title = "test"
                };
                hotelRepository.InsertOrUpdate(hotel);
                hotelRepository.Save();
                return hotel;
            }
        }

        public static BoundedReservationsHotel InsertBoundedReservationsHotel()
        {
            _hotelNumbersUpdateService = new HotelNumbersUpdateService();
            using (var hotelRepository = new HotelRepository(_hotelNumbersUpdateService))
            {
                var hotel = new BoundedReservationsHotel
                {
                    Address = "test",
                    Title = "test"
                };
                hotelRepository.InsertOrUpdate(hotel);
                hotelRepository.Save();
                return hotel;
            }
        }

        public static HotelNumber InsertHotelNumberForFreeReservationsHotel()
        {
            var hotel = InsertFreeReservationsHotel();
            _hotelNumbersUpdateService = new HotelNumbersUpdateService();
            return InsertHotelNumberForHotel(hotel);
        }

        public static HotelNumber InsertHotelNumberForBoundedReservationsHotel()
        {
            var hotel = InsertBoundedReservationsHotel();
            _hotelNumbersUpdateService = new HotelNumbersUpdateService();
            return InsertHotelNumberForHotel(hotel);
        }

        private static HotelNumber InsertHotelNumberForHotel(Hotel hotel)
        {
            using (var hotelNumbersRepository = new HotelNumbersRepository(_hotelNumbersUpdateService))
            {
                var hotelNumber = new HotelNumber
                                      {
                                          HotelId = hotel.Id,
                                          HotelNumberType = HotelNumberType.Double
                                      };
                hotelNumbersRepository.InsertOrUpdate(hotelNumber);
                hotelNumbersRepository.Save();
                return hotelNumber;
            }
        }
    }
}
