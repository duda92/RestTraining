using System.Collections.Generic;

namespace RestTraining.Api.Domain.Entities
{
    public class HotelNumber
    {
        public HotelNumber()
        {
            WindowViews = new List<WindowView>();
            IncludeItems = new List<IncludedItem>();
        }

        public int Id { get; set; }

        public List<WindowView> WindowViews { get; set; }

        public List<IncludedItem> IncludeItems { get; set; }

        public HotelNumberType HotelNumberType { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }
}
