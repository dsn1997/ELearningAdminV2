using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;

namespace IIG.Web.Data.Models.MockTests;

public class MockTestStructureWithQuestionTagModel : MockTestStructureModel
{
    public new List<MockTestSectionStructureWithQuestionTagModel> MockTestSections { get; set; }
}

public class MockTestSectionStructureWithQuestionTagModel : MockTestSectionStructureModel
{
    public new List<MockTestPartStructureWithQuestionTagModel> Parts { get; set; }
}
public class MockTestPartStructureWithQuestionTagModel : MockTestPartStructureModel
{
    public List<string> QuestionTags { get; set; }
}
