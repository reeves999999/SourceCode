namespace SourceCode.Web.Models
{
    public class PageAndSortViewModel
    {
        public string Sort { get; set; }

        public int Page { get; set; }

        public int PageSize { get; set; }

        public bool HasNextPage { get; set; }

        public bool HasPreviousPage { get; set; }

        public int ItemCount { get; set; }

        public int FilteredItemsCount { get; set; }

        public int TotalItems { get; set; }

        public string Search { get; set; }

        public int TotalPages
        {
            get
            {
                if (FilteredItemsCount > 0 && PageSize > 0)
                {
                    return (FilteredItemsCount / PageSize) + 1;
                }
                return 1;
            }
        }

        public string PagingStatusMessage
        {
            get
            {
                int lowerRange = Page > 1 ? (PageSize * (Page - 1)) + 1 : 1;
                int upperRange = (PageSize * Page) > FilteredItemsCount ? FilteredItemsCount : (PageSize * Page);

                return $"Showing {lowerRange} to {upperRange} of {FilteredItemsCount}";
            }
        }

        public void SetPagingState(int itemCount, int totalItemsCount, int pageSize, int page, string sort, int? filteredItemsCount = null, string search = "")
        {
            TotalItems = totalItemsCount;
            ItemCount = itemCount;
            PageSize = pageSize;
            Page = page;
            Sort = sort;
            Search = search;
            FilteredItemsCount = filteredItemsCount.HasValue ? filteredItemsCount.Value : totalItemsCount;
            HasNextPage = page * pageSize < FilteredItemsCount;
            HasPreviousPage = page > 1;
        }
    }
}
