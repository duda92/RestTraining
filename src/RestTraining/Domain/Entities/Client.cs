using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.Domain.Entities
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }
    }
}
