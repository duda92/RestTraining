using System;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.DTO
{
    public class FreeBookingDTO
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public int HotelNumberId { get; set; }

        public Client Client { get; set; }
        
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }
    }
}