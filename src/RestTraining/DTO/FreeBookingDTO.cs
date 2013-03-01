using System;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.DTO
{
    public class FreeBookingDTO
    {

        public FreeBookingDTO()
        {
            Client = new ClientDTO();
        }

        public int Id { get; set; }

        public int HotelId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "You must select hotel number for booking")]
        public int HotelNumberId { get; set; }

        public ClientDTO Client { get; set; }

        [DataType(DataType.Date)]
        public DateTime BeginDate { get; set; }
        
        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; }
    }
}