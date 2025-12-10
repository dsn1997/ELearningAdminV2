using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models
{
    public class LiveClassTimeTableRequest : BasePaginationRequest
    {
        public Guid LiveClassDetailId { get; set; }
    }
}
