using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RestTraining.Domain;

namespace RestTraining.Api.Domain.Entities
{
    public class WindowView
    {
        [Key]
        public WindowViewType Type { get; set; }

        public virtual List<HotelNumber> HotelNumbers { get; set; }
    }
}