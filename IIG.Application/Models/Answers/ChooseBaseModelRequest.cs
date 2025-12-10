using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;

namespace IIG.Application.Models;

public class ChooseBaseModelRequest
{
    public EQuestionnaireType QuestionnaireType { get; set; }
    
    public Guid AnswerId { get; set; }

    public Guid QuestionId { get; set; }

    public string AnswerText { get; set; }

    public Guid? MatchingQuestionId { get; set; }

    public FileInsertModel? AudioFileInfo { get; set; }
}