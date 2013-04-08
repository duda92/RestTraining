using RestTraining.Common.DataAnotations;
using System;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Common.DTO
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