using IIG.Core.Common.Models.Paging;

namespace IIG.Application.Models.ExamTool;
public class SearchingExamToolRequest : BasePaginationRequest
{
    public Guid? CategoryId { get; set; }

    public Guid? IgnoreId { get; set; }
}


public class FilterExamToolRequest : BasePaginationRequest
{
    public Guid? CategoryId { get; set; }

    public string? NameNonAscii { get; set; }
}
