using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.DTO
{
    public class ClientDTO
    {
        [Required(ErrorMessage = "You must enter your name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "You must enter your phone number")]
        public string PhoneNumber { get; set; }
    }
}