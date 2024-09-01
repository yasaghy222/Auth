namespace Auth.Contracts.Common
{
    public interface IPaginationFilterDto
    {
        public int? PageSize { get; set; }
        public int? PageIndex { get; set; }
    }
}