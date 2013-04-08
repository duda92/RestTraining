using System.Collections.Generic;
using RestTraining.Common.DTO;

namespace RestTraining.Web.Models
{
    public class BoundedBookingViewModel
    {
        public BoundedBookingDTO BoundedBooking { get; set; }

        public List<HotelNumberDTO> HotelNumbers { get; set; }

        public List<BoundedPeriodDTO> BoundedPeriods { get; set; }
    }
}