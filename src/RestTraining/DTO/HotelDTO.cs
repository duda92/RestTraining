using System.Collections.Generic;

namespace RestTraining.Api.DTO
{
    public class HotelDTO
    {
        public enum TypeDescriminator
        {
            Free,
            Bounded
        }

        public TypeDescriminator Type { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public List<HotelNumberDTO> HotelNumbers { get; set; }

        public HotelDTO()
        {
            HotelNumbers = new List<HotelNumberDTO>();
        }
    }
}