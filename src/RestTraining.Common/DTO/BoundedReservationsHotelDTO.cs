using System.Collections.Generic;

namespace RestTraining.Common.DTO
{
    public class BoundedReservationsHotelDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public List<HotelNumberDTO> HotelNumbers { get; set; }

        public List<HotelsAttractionDTO> HotelsAttractions { get; set; }

        public BoundedReservationsHotelDTO()
        {
            HotelNumbers = new List<HotelNumberDTO>();
            HotelsAttractions = new List<HotelsAttractionDTO>();
        }

        public string Description { get; set; }

        public byte[] Image { get; set; }
    }
}