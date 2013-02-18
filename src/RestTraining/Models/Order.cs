using System;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Domain
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public DateTime BeginDate { get; set; }

        public DateTime EndDate { get; set; }

        public Client Client { get; set; }

        public HotelNumber HotelNumber { get; set; }
    }
}
