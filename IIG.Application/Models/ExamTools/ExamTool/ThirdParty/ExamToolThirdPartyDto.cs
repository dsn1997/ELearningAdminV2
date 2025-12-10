namespace IIG.Application.Models.ExamTool.ThirdParty;

public class ExamToolThirdPartyDto
{
    public string UrlLearning { get; set; }
    
    public string CourseGuide { get; set; }

    public IEnumerable<ExamToolThirdPartyAccountDto> Accounts { get; set; }
}