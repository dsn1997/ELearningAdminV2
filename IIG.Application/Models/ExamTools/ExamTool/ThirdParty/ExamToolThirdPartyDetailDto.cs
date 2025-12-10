using IIG.Application.Models;

namespace IIG.Application.Models.ExamTool.ThirdParty;

public class ExamToolThirdPartyDetailDto
{
    public string CourseName { get; set; }

    public List<CoursePriceBasicDto> CoursePrices { get; set; } = new();

    public string UserGuideLink { get; set; }
}