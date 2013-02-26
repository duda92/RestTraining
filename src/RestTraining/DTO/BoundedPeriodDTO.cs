using System;

namespace RestTraining.Api.DTO
{
    public class BoundedPeriodDTO
    {
        public int Id { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public int BoundedReservationsHotelId { get; set; }
    }
}