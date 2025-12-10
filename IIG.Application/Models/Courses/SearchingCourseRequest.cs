using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models;

public class SearchingCourseRequest : BasePaginationRequest
{
    public Guid? CategoryId { get; set; }

    public Guid? IgnoreId { get; set; }

    public EProductType ProductType { get; set; } = EProductType.SelfStudyCourse;

}

public class FilterCourseRequest : BasePaginationRequest
{
    public Guid? CategoryId { get; set; }

    public string? NameNonAscii { get; set; }

    public EProductType ProductType { get; set; } = EProductType.SelfStudyCourse;

}