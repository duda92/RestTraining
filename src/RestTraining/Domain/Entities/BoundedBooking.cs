namespace RestTraining.Api.Domain.Entities
{
    public class BoundedBooking : Booking
    {
        public BoundedBooking()
        {
            BoundedPeriod = new BoundedPeriod();
        }

        public BoundedPeriod BoundedPeriod { get; set; }
    }
}