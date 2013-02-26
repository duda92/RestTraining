namespace RestTraining.Api.Domain.Entities
{
    public class BoundedBooking : Booking
    {
        public BoundedPeriod BoundedPeriod { get; set; }
    }
}