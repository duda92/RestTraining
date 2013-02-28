using System;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.DTO
{
    public class BoundedPeriodDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }

        public int BoundedReservationsHotelId { get; set; }
    }
}