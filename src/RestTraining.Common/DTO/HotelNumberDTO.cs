using System.Collections.Generic;

namespace RestTraining.Common.DTO
{
    public class HotelNumberDTO
    {
        public HotelNumberDTO()
        {
            WindowViews = new List<WindowViewTypeDTO>();
            IncludeItems = new List<IncludedItemDTO>();
        }

        public int Id { get; set; }

        public List<WindowViewTypeDTO> WindowViews { get; set; }

        public List<IncludedItemDTO> IncludeItems { get; set; }

        public HotelNumberTypeDTO HotelNumberType { get; set; }

        public int HotelId { get; set; }
    }
}