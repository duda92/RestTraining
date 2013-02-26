namespace RestTraining.Api.Domain.Entities
{
    public class IncludedItem
    {
        public int Id { get; set; }

        public IncludeItemType IncludeItemType { get; set; }

        public int Count { get; set; }
    }
}