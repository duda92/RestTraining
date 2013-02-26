using System.Linq;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Services
{
    public class HotelNumbersUpdateService : IHotelNumbersUpdateService
    {
        public void PreInsertOrUpdateHotel(RestTrainingApiContext context, Hotel hotel)
        {
            foreach (var hotelNumber in hotel.HotelNumbers)
            {
                PreInsertOrUpdateHotelNumber(context, hotelNumber);
            }
        }

        public void PreInsertOrUpdateHotelNumber(RestTrainingApiContext context, HotelNumber hotelNumber)
        {
            for (var i = 0; i < hotelNumber.WindowViews.Count; i++)
            {
                var type = hotelNumber.WindowViews[i].Type;
                var existedWindowView = context.WindowViews.SingleOrDefault(x => x.Type == type);
                hotelNumber.WindowViews[i] = existedWindowView;
            }
        }
    }
}