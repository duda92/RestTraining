﻿using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.DTO
{
    public class ClientDTO
    {
        [Required(ErrorMessage = "Name is invalid")]
        public string Name { get; set; }

        [Required(ErrorMessage = "PhoneNumber is invalid")]
        public string PhoneNumber { get; set; }
    }
}