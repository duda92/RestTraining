using System.Collections.Generic;

namespace RestTraining.Common.DTO
{
    public class HotelNumbersSearchQuery
    {
        public HotelNumbersSearchQuery()
        {
            IncludeItems = new List<IncludedItemDTO>();
            WindowViewTypes = new List<WindowViewTypeDTO>();
            HotelsAttractions = new List<HotelsAttractionDTO>();
        }

        public List<IncludedItemDTO> IncludeItems { get; set; }

        public List<WindowViewTypeDTO> WindowViewTypes { get; set; }

        public List<HotelsAttractionDTO> HotelsAttractions { get; set; }
    }
}
