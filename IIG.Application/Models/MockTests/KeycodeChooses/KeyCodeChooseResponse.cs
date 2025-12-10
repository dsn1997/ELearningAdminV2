using IIG.Web.Data.Models.Answers;

namespace IIG.Web.Data.Models.MockTests.KeycodeChooses;

public class KeyCodeChooseResponse : ChooseBaseModelResponse
{
    public string KeyCode { get; set; }
    
    public Guid MockTestPartId { get; set; }
    
    public Guid MockTestSectionId { get; set; }
}

public class KeyCodeAnswerModel
{
 
    public List<Guid> ListQuestionId { get; set; }

    public List<Guid> ListQuestionnareId { get; set; }
}