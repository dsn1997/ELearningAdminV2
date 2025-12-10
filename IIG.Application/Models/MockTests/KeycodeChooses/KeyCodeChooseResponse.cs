using IIG.Application.Models;

namespace IIG.Application.Models.KeycodeChooses;

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