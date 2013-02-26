using System.Collections.Generic;
using RestTraining.Api.Domain.Entities;
using RestTraining.Domain;

namespace RestTraining.Api.DTO
{
    public class HotelNumberDTO
    {
        public HotelNumberDTO()
        {
            WindowViews = new List<WindowViewType>();
            IncludeItems = new List<IncludedItemDTO>();
        }

        public int Id { get; set; }

        public List<WindowViewType> WindowViews { get; set; }

        public List<IncludedItemDTO> IncludeItems { get; set; }

        public HotelNumberType HotelNumberType { get; set; }

        public int HotelId { get; set; }
    }
}