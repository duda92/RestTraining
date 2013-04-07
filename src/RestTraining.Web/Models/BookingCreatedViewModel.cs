using System;
using RestTraining.Api.DTO;

namespace RestTraining.Web.Models
{
    public class BookingCreatedViewModel
    {
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public HotelNumberDTO HotelNumber { get; set; }

        public HotelDTO Hotel { get; set; }

        public ClientDTO Client { get; set; }

        public int BookingId { get; set; }
    }
}