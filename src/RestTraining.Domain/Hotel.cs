using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Domain
{
    public abstract class Hotel
    {
        public Hotel()
        {
            HotelNumbers = new List<HotelNumber>();
        }

        [Key]
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public List<HotelNumber> HotelNumbers { get; set; }
    }
}
