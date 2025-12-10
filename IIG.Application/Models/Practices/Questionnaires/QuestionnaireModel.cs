using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.Practices.Questionnaires;
public class QuestionnaireModel
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public EQuestionnaireType Type { get; set; }

    public Guid? GroupId { get; set; }

    public string IntroContent { get; set; }

    public bool IsActive { get; set; }

    public string TitleLeftSection { get; set; }

    public Guid? VideoFileId { get; set; }

    public Guid? SubtitleFileId { get; set; }

    public Guid? SlideFileId { get; set; }
}
