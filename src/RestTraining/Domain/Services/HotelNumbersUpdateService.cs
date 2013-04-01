using System.Linq;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Services
{
    public class HotelNumbersUpdateService : IHotelNumbersUpdateService
    {
        //Inserts windows views for each hotel number in hotel
        //todo: change it to stored procedure
        public void PreInsertOrUpdateHotel(RestTrainingApiContext context, Hotel hotel)
        {
            foreach (var hotelNumber in hotel.HotelNumbers)
            {
                PreInsertOrUpdateHotelNumber(context, hotelNumber);
            }
        }
        
        //Inserts windows views for hotel number 
        //todo: change it to stored procedure
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