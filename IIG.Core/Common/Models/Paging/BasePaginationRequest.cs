using IIG.Core.Common.ConfigureModels;
using IIG.Core.Helpers;
using System.Text.Json.Serialization;

namespace IIG.Core.Common.Models.Paging;

public abstract class BasePaginationRequest : BasePaginationRequest<Enum>
{ }

public abstract class BasePaginationRequest<T> where T : Enum
{
    public string Keyword { get; set; }
    public int PageNum { get; set; } = Constants.Pagination.DefaultPage;
    public int PageSize { get; set; } = Constants.Pagination.ItemsPerPage;

    public bool Descending { get; set; }
    public T SortColumn { get; set; }

    [JsonIgnore]
    public string KeywordFullTextQuery
    {
        get
        {
            return Keyword.TranslateToFullTextSearchQuery();
        }
    }
}