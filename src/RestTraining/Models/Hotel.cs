using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Domain
{
    public abstract class Hotel
    {
        [Key]
        public int Id { get; set; }

        public List<HotelNumber> HotelNumbers { get; set; }
    }
}
