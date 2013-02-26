using System.Collections.Generic;

namespace RestTraining.Api.Domain.Entities
{
    public class BoundedReservationsHotel : Hotel
    {
        public BoundedReservationsHotel()
        {
            BoundedPeriods = new List<BoundedPeriod>();
        }

        public List<BoundedPeriod> BoundedPeriods { get; set; }
    }
}
