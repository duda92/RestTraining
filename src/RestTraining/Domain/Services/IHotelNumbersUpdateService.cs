using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Services
{
    public interface IHotelNumbersUpdateService
    {
        void PreInsertOrUpdateHotel(RestTrainingApiContext context, Hotel hotel);
        void PreInsertOrUpdateHotelNumber(RestTrainingApiContext context, HotelNumber hotelNumber);
    }
}