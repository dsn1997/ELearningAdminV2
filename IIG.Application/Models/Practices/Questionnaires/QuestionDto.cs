using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using System.Text.Json.Serialization;

namespace IIG.Application.Models.Questionnaires
{
    public class QuestionDto : 
        IAudioFileDuration, IAudioFileProperty,
        ISpaceTimeProperty, IRecordingSettingProperty, IMaxWritingLengthProperty
    {
        public string Id { get; set; }

        public Guid QuestionId { get; set; }

        public string Name { get; set; }

        public Guid QuestionnaireId { get; set; }

        public int? SortOrder { get; set; }

        public string Explanation { get; set; }

        public string Note { get; set; }

        [JsonIgnore]
        public string FakeValues { get; set; }

        public List<string> DragDropValues { get; set; }

        /// <summary>
        /// Video timestamp
        /// </summary>
        public string DisplayTimestamp { get; set; }

        public Guid? ImageFileId { get; set; }
        public FileDto ImageFileInfo { get; set; }

        public string WordEnglish { get; set; }

        public EQuestionTypeOfWord TypeOfWord { get; set; }

        public string WordPhonetic { get; set; }

        public Guid? WordFileId { get; set; }

        public Guid? TextFileId { get; set; }

        public string TextEnglish { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        [JsonIgnore]
        public string MatchingJson { get; set; }

        [JsonIgnore]
        public string SampleTemplateJsonObject { get; set; }


        public AnswerQuestionMatching AnswerQuestionMatching { get; set; }

        public List<AnswerDto> Answers { get; set; } = new();
        public List<QuestionTranslationDto> Translations { get; set; }


        public long? AudioDuration { get; set; }
        public List<SampleTemplateViewModel> SampleTemplateViewModels{ get; set; }
        public int? MaxWritingLength { get; set; }
        public TimeSpan? RecordingTime { get; set; }
        public TimeSpan? SpaceTime { get; set; }

        //Id Audio question S&W
        public Guid? AudioFileId { get; set; }

        public SubmittedQuestionModel SubmittedQuestionModel { get; set; }
    }
    

    public class AnswerDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid QuestionId { get; set; }

        public Guid? ImageFileId { get; set; }
        public FileDto ImageFileInfo { get; set; }

        public string MatchingKey { get; set; }

        public List<string> DropListValue { get; set; }

        public int? SortOrder { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        [JsonIgnore]
        public bool? CorrectMCQ { get; set; }

        [JsonIgnore]
        public string CorrectMatchingValues { get; set; }

        [JsonIgnore]
        public string FakeSelectValues { get; set; }
    }

    public class QuestionTranslationDto
    {
        public Guid QuestionId { get; set; }
        public string WordTranslation { get; set; }
        public string LanguageCode { get; set; }
    }

    public class AnswerQuestionMatching
    {
        public List<MatchingQuestionJson> Questions { get; set; }
        public List<MatchingAnswerJson> Answers { get; set; }

    }
    public class MatchingQuestionJson
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ImageFileId { get; set; }

        public FileDto ImageFileInfo { get; set; }
    }

    public class MatchingAnswerJson
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid QuestionId { get; set; }
    }
}