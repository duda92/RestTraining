using System;
using System.ComponentModel.DataAnnotations;
using RestTraining.Api.Infrastructure;

namespace RestTraining.Api.DTO
{
    public class FreeBookingDTO
    {
        public FreeBookingDTO()
        {
            Client = new ClientDTO();
            BeginDate = DateTime.Today;
            EndDate = DateTime.Today.AddDays(7);
        }

        public int Id { get; set; }

        public int HotelId { get; set; }

        [Required]
        [Range(1, Int32.MaxValue, ErrorMessage = "You must select hotel number for booking")]
        public int HotelNumberId { get; set; }

        public ClientDTO Client { get; set; }

        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2013", "3/4/2050",  ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime BeginDate { get; set; }
        
        [DataType(DataType.Date)]
        [Range(typeof(DateTime), "1/1/2013", "3/4/2050", ErrorMessage = "Value for {0} must be between {1} and {2}")]
        [DateGreaterThanAttribute("BeginDate")]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
    }
}