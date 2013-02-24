using System;

namespace RestTraining.Api.Models
{
    public class BoundedPeriod
    {
        public int Id { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public int BoundedReservationsHotelId { get; set; }
    }
}