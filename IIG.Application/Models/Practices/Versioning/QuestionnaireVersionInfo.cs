using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Application.Models.Questionnaires;

namespace IIG.Application.Models.Versioning;
public class QuestionnaireVersionInfo
{
    public Guid QuestionnaireId { get; set; }

    public string Name { get; set; }

    public EQuestionnaireType Type { get; set; }

    public Guid? GroupId { get; set; }

    public bool IsActive { get; set; }

    public string TitleLeftSection { get; set; }

    public Guid? VideoFileId { get; set; }

    public Guid? SubtitleFileId { get; set; }

    public Guid? SlideFileId { get; set; }

    public FileDto SubtitleInfo { get; set; }

    public FileDto SlideInfo { get; set; }

    public string IntroContent { get; set; }

    public string Identifier { get; set; }

    public List<QuestionVersionInfo> Questions { get; set; } = new List<QuestionVersionInfo>();

    public List<LeftSectionVersionInfo> LeftSections { get; set; } = new List<LeftSectionVersionInfo>();
}
