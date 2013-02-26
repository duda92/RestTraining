using System;

namespace RestTraining.Api.Domain.Entities
{
    public class FreeBooking : Booking
    {
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}