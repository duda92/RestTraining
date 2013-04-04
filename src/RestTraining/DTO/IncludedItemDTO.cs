using RestTraining.Api.Domain.Entities;
using System.ComponentModel.DataAnnotations;

namespace RestTraining.Api.DTO
{
    public class IncludedItemDTO
    {
        public IncludeItemType IncludeItemType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid count of included item")]
        public int Count { get; set; }
    }
}