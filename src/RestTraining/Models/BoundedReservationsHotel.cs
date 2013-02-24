using System.Collections.Generic;
using RestTraining.Api.Models;

namespace RestTraining.Domain
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
