using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace RestTraining.Domain
{
    public class HotelNumber
    {
        public HotelNumber()
        {
            WindowViews = new List<WindowView>();
            IncludeItems = new List<IncludeItem>();
        }

        public int Id { get; set; }

        public List<WindowView> WindowViews { get; set; }

        public List<IncludeItem> IncludeItems { get; set; }

        public HotelNumberType HotelNumberType { get; set; }

        public int HotelId { get; set; }

        public Hotel Hotel { get; set; }
    }

    public class WindowView
    {
        [Key]
        public WindowViewType Type { get; set; }
        
        [JsonIgnore]
        public List<HotelNumber> HotelNumbers { get; set; }
    }

    public class IncludeItem
    {
        public int Id { get; set; }

        public IncludeItemType IncludeItemType { get; set; }

        public int Count { get; set; }
    }
}
