namespace ElmBookShelf.Domain.QueryOptions
{
    public class QueryOption
    {
        public long? Id { get; set; }
        public string SearchKey { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
