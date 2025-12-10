using IIG.Core.Common.Enums;

namespace IIG.Application.Models.Questionnaires;
public class FlashCardQuestionModel
{
    public Guid Id { get; set; }

    public string WordEnglish { get; set; }

    public EQuestionTypeOfWord TypeOfWord { get; set; }

    public string WordPhonetic { get; set; }

    public Guid? WordFileId { get; set; }

    public Guid? TextFileId { get; set; }

    public string TextEnglish { get; set; }

    public List<QuestionTranslationDto> Translations = new();
}