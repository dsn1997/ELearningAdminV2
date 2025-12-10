using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models;

public class SearchingNewsByCategoryIdRequest : BasePaginationRequest
{
    public Guid? CategoryId { get; set; }
    public Guid? IngnoreNewsId { get; set; }
}