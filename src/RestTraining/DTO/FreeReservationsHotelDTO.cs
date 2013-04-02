﻿using System.Collections.Generic;

namespace RestTraining.Api.DTO
{
    public class FreeReservationsHotelDTO
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Address { get; set; }

        public List<HotelNumberDTO> HotelNumbers { get; set; }

        public FreeReservationsHotelDTO()
        {
            HotelNumbers = new List<HotelNumberDTO>();
        }

        public string Description { get; set; }

        public byte[] Image { get; set; }
    }
}