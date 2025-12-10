using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.News;

public class SearchingNewsByCategoryIdRequest : BasePaginationRequest
{
    public Guid? CategoryId { get; set; }
    public Guid? IngnoreNewsId { get; set; }
}