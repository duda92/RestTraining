using System;
using System.ComponentModel.DataAnnotations;
using RestTraining.Api.Infrastructure;

namespace RestTraining.Api.DTO
{
    public class BoundedPeriodDTO
    {
        public int Id { get; set; }

        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }

        [DataType(DataType.Date)]
        [DateGreaterThanAttribute("BeginDate")] 
        public DateTime EndDate { get; set; }

        public int BoundedReservationsHotelId { get; set; }
    }

}