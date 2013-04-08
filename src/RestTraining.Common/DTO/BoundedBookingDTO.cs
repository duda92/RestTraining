using System.ComponentModel.DataAnnotations;

namespace RestTraining.Common.DTO
{
    public class BoundedBookingDTO
    {
        public int Id { get; set; }

        [Range(1, int.MaxValue, ErrorMessage="HotelId is invalid")]
        public int HotelId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "HotelNumberId is invalid")]
        public int HotelNumberId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "BoundedPeriodId is invalid")]
        public int BoundedPeriodId { get; set; }

        public ClientDTO Client { get; set; }
    }
}