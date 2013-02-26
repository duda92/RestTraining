using System.Collections.Generic;
using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.DTO
{
    public class HotelDTO
    {
        public enum TypeDescriminator
        {
            Free,
            Bounded
        }

        public TypeDescriminator Type { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public List<HotelNumber> HotelNumbers { get; set; }

        public HotelDTO()
        {
            HotelNumbers = new List<HotelNumber>();
        }
    }
}