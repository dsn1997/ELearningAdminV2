namespace IIG.Application.Models.UnitTest;
public class UnitTestSeeAnswersModel
{
    public string UnitTestTitle { get; set; }

    public List<Guid> QuestionnaireIds { get; set; }

    public List<UnitTestVersionMenuWithStatusListModel> Menu { get; set; }

    public UnitTestSpeakingWritingResult? SpeakingWritingResult { get; set; }
}


public class UnitTestSpeakingWritingResult
{
    public Guid? CourseScoringId { get; set; }
    public List<Guid> QuestionnaireIds { get; set; } = new();
}
