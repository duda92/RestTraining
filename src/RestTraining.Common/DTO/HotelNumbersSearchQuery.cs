using System.Collections.Generic;

namespace RestTraining.Common.DTO
{
    public class HotelNumbersSearchQuery
    {
        public HotelNumbersSearchQuery()
        {
            IncludeItems = new List<IncludedItemDTO>();
            WindowViews = new List<WindowViewTypeDTO>();
            HotelsAttractions = new List<HotelsAttractionDTO>();
        }

        public List<IncludedItemDTO> IncludeItems { get; set; }

        public List<WindowViewTypeDTO> WindowViews { get; set; }

        public List<HotelsAttractionDTO> HotelsAttractions { get; set; }
    }
}
