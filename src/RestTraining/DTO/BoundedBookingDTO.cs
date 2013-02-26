using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.DTO
{
    public class BoundedBookingDTO
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public int HotelNumberId { get; set; }

        public Client Client { get; set; }

        public BoundedPeriod BoundedPeriod { get; set; }
    }
}