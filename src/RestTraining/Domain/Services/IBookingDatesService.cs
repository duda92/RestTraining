using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.Domain.Services
{
    public interface IBookingDatesService
    {
        bool IsBoundedPeriodValid(RestTrainingApiContext context, BoundedPeriod boundedPeriod);

        bool IsFreeBookingValid(RestTrainingApiContext context, FreeBooking freeBooking);
        bool IsBoundedBookingValid(RestTrainingApiContext context, BoundedBooking boundedBooking);
    }
}