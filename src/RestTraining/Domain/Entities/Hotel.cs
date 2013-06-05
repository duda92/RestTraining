using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.Domain.Entities
{
    public abstract class Hotel
    {
        protected Hotel()
        {
            HotelNumbers = new List<HotelNumber>();
            HotelsAttractions = new List<HotelsAttraction>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public string Description { get; set; }

        public List<HotelNumber> HotelNumbers { get; set; }

        public byte[] Image { get; set; }

        public List<HotelsAttraction> HotelsAttractions { get; set; }

    }
}
