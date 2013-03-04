namespace RestTraining.Api.DTO
{
    public class BoundedBookingDTO
    {
        public int Id { get; set; }

        public int HotelId { get; set; }

        public int HotelNumberId { get; set; }

        public ClientDTO Client { get; set; }

        //public BoundedPeriod BoundedPeriod { get; set; }

        public int BoundedPeriodId { get; set; }
    }
}