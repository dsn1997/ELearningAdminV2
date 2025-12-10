using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.Practices.MyCourse;
public class MyCourseListForSelectionRequest : BasePaginationRequest
{
    public EProductType? ProductType { get; set; }
    public bool IsOnlyActive { get; set; }
}
