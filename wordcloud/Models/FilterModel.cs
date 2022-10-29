namespace wordcloud.Models
{
    public sealed class FilterModel
    {
        internal const int DEFAULT_PAGE = 1;
        internal const int DEFAULT_PAGESIZE = 50;

        public int Page { get; set; }
        public int PageSize { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        internal int SkipSize 
        { 
            get
            {
                return (Page - 1) * PageSize;
            }
        }

        public FilterModel(int page, int pageSize)
        {
            Page = page < 1 ? DEFAULT_PAGE : page;
            PageSize = pageSize < 1 ? DEFAULT_PAGESIZE : pageSize;
        }
        public FilterModel()
        {
            Page = DEFAULT_PAGE;
            PageSize = DEFAULT_PAGESIZE;
        }
    }
}
