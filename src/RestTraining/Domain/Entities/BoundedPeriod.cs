using System;

namespace RestTraining.Api.Domain.Entities
{
    public class BoundedPeriod
    {
        public int Id { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public int BoundedReservationsHotelId { get; set; }
    }
}