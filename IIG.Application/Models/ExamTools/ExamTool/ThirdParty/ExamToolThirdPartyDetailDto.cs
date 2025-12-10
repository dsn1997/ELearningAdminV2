using IIG.Web.Data.Models.CoursePrice;

namespace IIG.Web.Data.Models.ExamTools.ExamTool.ThirdParty;

public class ExamToolThirdPartyDetailDto
{
    public string CourseName { get; set; }

    public List<CoursePriceBasicDto> CoursePrices { get; set; } = new();

    public string UserGuideLink { get; set; }
}