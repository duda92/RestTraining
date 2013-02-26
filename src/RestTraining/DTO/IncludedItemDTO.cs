using RestTraining.Api.Domain.Entities;

namespace RestTraining.Api.DTO
{
    public class IncludedItemDTO
    {
        public IncludeItemType IncludeItemType { get; set; }

        public int Count { get; set; }
    }
}