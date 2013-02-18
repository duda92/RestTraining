using System.Collections.Generic;

namespace RestTraining.Domain
{
    public class HotelNumber
    {
        public List<WindowViewType> WindowsViews { get; set; }

        public List<IncludeItem> IncludeItems { get; set; }

        public HotelNumberType HotelNumberType { get; set; }
    }
}
