
namespace Restaurant.Application.Common;

public class PagedResult<T> where T : class
{
    public PagedResult(IEnumerable<T> items, int totalCount, int pageSize, int pageNumber )
    {
        Items = items;
        TotalCount = totalCount;
        PageSize = pageSize;
        TotalPages = (int)Math.Ceiling(totalCount / (double)PageSize);
        ItemsFrom = pageSize * (pageNumber - 1) + 1;
        ItemsTo = ItemsFrom + pageSize - 1; 
    }
    public IEnumerable<T> Items { get; set; } // the list of items i wanna display
    public int TotalCount { get; set; }
    public int TotalPages { get; set; }
    public int PageSize { get; set; }
    public int ItemsFrom { get; set; }
    public int ItemsTo { get; set; }

}
