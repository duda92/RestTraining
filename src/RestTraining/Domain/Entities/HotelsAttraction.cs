using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestTraining.Api.Domain.Entities
{
    public class HotelsAttraction
    {
        public int Id { get; set; }

        public HotelsAttractionType HotelsAttractionType { get; set; }

        public int Count { get; set; }

        public int HotelId { get; set; }
    }
}