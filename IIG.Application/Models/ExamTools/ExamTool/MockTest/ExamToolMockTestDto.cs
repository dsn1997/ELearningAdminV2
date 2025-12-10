using IIG.Core.Common.Models.Paging;

namespace IIG.Web.Data.Models.ExamTools.ExamTool.MockTest;

public class ExamToolMockTestDto
{
    public string Name { get; set; }
    
    public string UserGuideLink { get; set; }

    public PaginationSet<MockTestKeyCodeDto> MockTestKeyCodes { get; set; }

    public int? WatchCount { get; set; }
}