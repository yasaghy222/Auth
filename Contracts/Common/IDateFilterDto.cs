using Auth.Contracts.Enums;

namespace Auth.Contracts.Common
{
    public interface IDateFilterDto
    {
        public DateTime? FromCreateAt { get; set; }
        public QueryComparisonType FromCreateAtComparisonType { get; set; }


        public DateTime? ToCreateAt { get; set; }
        public QueryComparisonType ToCreateAtComparisonType { get; set; }

        public QueryOrderType? CreateAtOrderType { get; set; }
    }

    public record DateFilterDto : IDateFilterDto
    {
        public DateTime? FromCreateAt { get; set; }
        public QueryComparisonType FromCreateAtComparisonType { get; set; }
           = QueryComparisonType.GreaterThanOrEqualTo;

        public DateTime? ToCreateAt { get; set; }
        public QueryComparisonType ToCreateAtComparisonType { get; set; }
                 = QueryComparisonType.LessThanOrEqualTo;

        public QueryOrderType? CreateAtOrderType { get; set; }
    }
}