using IIG.Core.Common.Enums;
using Newtonsoft.Json;

namespace IIG.Web.Data.Models.Practices.Dictionary;
public class MyDictionaryListModel
{
    public Guid Id { get; set; }

    public Guid CourseId { get; set; }

    public Guid UnitId { get; set; }

    public Guid LessonId { get; set; }

    public Guid StepId { get; set; }

    public Guid QuestionnaireId { get; set; }

    public Guid QuestionId { get; set; }

    public string WordEnglish { get; set; }

    public EQuestionTypeOfWord TypeOfWord { get; set; }

    public string WordPhonetic { get; set; }

    public Guid? WordFileId { get; set; }

    public Guid? TextFileId { get; set; }

    public string TextEnglish { get; set; }

    public string WordTranslation { get; set; }

    public bool IsValid { get; set; }

    public int? Status { get; set; }
}