namespace RestTraining.Domain
{
    public class IncludeItem
    {
        public IncludeItemType IncludeItemType { get; set; }
    }

    public enum IncludeItemType
    {
        TvSet,
        Balcony,
        AirConditioner
    }
}
