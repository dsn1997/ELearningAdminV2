using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.LiveClass
{
    public class LiveClassTimeTableRequest : BasePaginationRequest
    {
        public Guid LiveClassDetailId { get; set; }
    }
}
