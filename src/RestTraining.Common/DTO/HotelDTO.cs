using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Common.DTO
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

        [Required(ErrorMessage = "Title is required")]
        public string Title { get; set; }
        
        [Required(ErrorMessage = "Address is required")]
        public string Address { get; set; }

        public string Description { get; set; }

        public List<HotelNumberDTO> HotelNumbers { get; set; }

        public byte[] Image { get; set; }

        public HotelDTO()
        {
            HotelNumbers = new List<HotelNumberDTO>();
        }
    }
}