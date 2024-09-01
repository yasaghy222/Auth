namespace Auth.Contracts.Response
{
    public record QueryResponse<TResponse>
    {
        public IEnumerable<TResponse> Items { get; set; } = [];

        public int Count { get; set; } = 0;
        public int TotalCount { get; set; } = 0;

        public int? PageSize { get; set; } = 10;
        public int? PageIndex { get; set; } = 1;
        public int TotalPageIndex { get; set; } = 0;
    }
}