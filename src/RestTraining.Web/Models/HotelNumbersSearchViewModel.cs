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
            Hotels = new List<HotelDTO>();
        }

        public HotelNumbersSearchQuery Query { get; set; }

        public List<HotelNumberDTO> Results { get; set; }

        public List<HotelDTO> Hotels { get; set; }

    }
}