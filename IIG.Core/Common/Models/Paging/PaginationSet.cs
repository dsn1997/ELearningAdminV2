namespace IIG.Core.Common.Models.Paging;
public class PaginationSet<T>
{
    public int PageNum { set; get; } = 1;

    public int PageSize { set; get; } = 20;

    public int TotalRecords { set; get; } = 0;

    public IEnumerable<T> Items { set; get; }
}
