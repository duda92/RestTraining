using System;
using RestTraining.Domain;

namespace RestTraining.Api.Models
{
    public abstract class Booking
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public HotelNumber HotelNumber { get; set; }

        public Client Client { get; set; }
    }

    public class BoundedBooking : Booking
    {
        public BoundedPeriod BoundedPeriod { get; set; }
    }

    public class FreeBooking : Booking
    {
        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

    }
}