using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Helpers;
using IIG.Application.Models.Questionnaires;
using Newtonsoft.Json;

namespace IIG.Application.Models.Versioning;
public class QuestionVersionInfo
{
    public Guid QuestionId { get; set; }

    public string Name { get; set; }

    public Guid QuestionnaireId { get; set; }

    public int? SortOrder { get; set; }

    public string Explanation { get; set; }

    public string Note { get; set; }

    public string FakeValues { get; set; }

    [JsonIgnore]

    public string[] DragDropValues { get; set; }

    public string DisplayTimestamp { get; set; }

    public Guid? ImageFileId { get; set; }

    public FileDto ImageFileInfo { get; set; }

    public string WordEnglish { get; set; }

    public EQuestionTypeOfWord? TypeOfWord { get; set; }

    public string WordPhonetic { get; set; }

    public Guid? WordFileId { get; set; }

    public Guid? TextFileId { get; set; }

    public string TextEnglish { get; set; }

    public List<AnswerVersionInfo> Answers { get; set; }

    // use for displaying on website
    public VersionAnswerQuestionMatching AnswerQuestionMatching { get; set; }

    // use for checking data user submit
    public List<VersionCorrectAnswerQuestionMatching> CorrectAnswerQuestionMatchings { get; set; }

    public List<QuestionTranslationDto> Translations { get; set; } = new();

    [JsonIgnore]
    public string MatchingJson { get; set; }
    public string SampleTemplateJsonObject;
    public List<SampleTemplateViewModel> SampleTemplateViewModels { get { return SampleTemplateJsonObject.ConvertSampleTemplate<SampleTemplateViewModel>()?.OrderBy(x => x?.SortOrder ?? -1)?.ToList(); } }

    public SubmittedQuestionModel SubmittedQuestionModel { get; set; }
    public Guid? AudioFileId { get; set; }
}

public class AnswerVersionInfo
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid QuestionId { get; set; }

    public Guid? ImageFileId { get; set; }

    public FileDto ImageFileInfo { get; set; }

    public bool? CorrectMCQ { get; set; }

    public string MatchingKey { get; set; }

    public string CorrectMatchingValues { get; set; }

    public string FakeSelectValues { get; set; }

    public string[] DropListValue { get; set; }

    public int? SortOrder { get; set; }
}

public class VersionMatchingJsonModel
{
    public MatchingVersionQuestionJson MatchingQuestion { get; set; }

    public MatchingVersionAnswerJson Answer { get; set; }

}

public class VersionAnswerQuestionMatching
{
    public List<MatchingVersionQuestionJson> Questions { get; set; }

    public List<MatchingVersionAnswerJson> Answers { get; set; }
}

public class VersionCorrectAnswerQuestionMatching
{
    public Guid MatchingQuestionId { get; set; }

    public Guid AnswerId { get; set; }
}

public class MatchingVersionQuestionJson
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid? ImageFileId { get; set; }

    public FileDto ImageFileInfo { get; set; }
}

public class MatchingVersionAnswerJson
{
    public Guid Id { get; set; }

    public string Name { get; set; }

    public Guid QuestionId { get; set; }
}
