using System.ComponentModel.DataAnnotations;

namespace RestTraining.Common.DTO
{
    public class IncludedItemDTO
    {
        public IncludeItemTypeDTO IncludeItemType { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Invalid count of included item")]
        public int Count { get; set; }
    }
}