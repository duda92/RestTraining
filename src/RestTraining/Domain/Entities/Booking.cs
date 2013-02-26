namespace RestTraining.Api.Domain.Entities
{
    public abstract class Booking
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public HotelNumber HotelNumber { get; set; }

        public int HotelNumberId { get; set; }

        public Client Client { get; set; }

        public int ClientId { get; set; }
    }
}