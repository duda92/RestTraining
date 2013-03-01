using System.Collections.Generic;
using RestTraining.Api.DTO;

namespace RestTraining.Web.Models
{
    public class FreeBookingViewModel
    {
        public FreeBookingDTO FreeBooking { get; set; }

        public List<HotelNumberDTO> HotelNumbers { get; set; }
    }
}