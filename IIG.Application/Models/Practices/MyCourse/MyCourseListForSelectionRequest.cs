using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models.MyCourse;
public class MyCourseListForSelectionRequest : BasePaginationRequest
{
    public EProductType? ProductType { get; set; }
    public bool IsOnlyActive { get; set; }
}
