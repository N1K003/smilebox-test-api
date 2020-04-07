namespace Smilebox.BusinessLogic.Contracts.Models.Common
{
    public class FilterModel
    {
        public int? Limit { get; set; }
        public int? Offset { get; set; }
        public SortDirection SortDirection { get; set; } = SortDirection.Asc;
    }

    public enum SortDirection
    {
        Asc,
        Desc
    }
}