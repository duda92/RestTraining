using System.Collections.Generic;
using RestTraining.Common.DTO;

namespace RestTraining.Web.Models
{
    public class HotelNumbersSearchViewModel
    {
        public HotelNumbersSearchViewModel()
        {
            Query = new HotelNumbersSearchQuery();
            Results = new List<HotelNumberDTO>();
        }

        public HotelNumbersSearchQuery Query { get; set; }

        public List<HotelNumberDTO> Results { get; set; }
    }
}