using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using IIG.Core.Common.Models.SpeakingAndWriting;
using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels
{
    [BsonCollection("questions")]
    public class MgQuestionModel : Document,
             IAudioFileDuration,
            ISpaceTimeProperty, IRecordingSettingProperty, IMaxWritingLengthProperty,
            ISampleTemplateProperty,
            IAudioFileProperty
    {
        public Guid QuestionId { get; set; }

        public string Name { get; set; }

        public Guid QuestionnaireId { get; set; }

        public int? SortOrder { get; set; }

        public string Explanation { get; set; }

        public string Note { get; set; }

        public string FakeValues { get; set; }
        public string[] DragDropValues { get; set; }

        /// <summary>
        /// Video timestamp
        /// </summary>
        public string DisplayTimestamp { get; set; }

        public Guid? ImageFileId { get; set; }
        public FileDto ImageFileInfo { get; set; }

        public string WordEnglish { get; set; }

        public EQuestionTypeOfWord? TypeOfWord { get; set; }

        public string WordPhonetic { get; set; }

        public Guid? WordFileId { get; set; }

        public Guid? TextFileId { get; set; }

        public string TextEnglish { get; set; }

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }

        // use for displaying on website
        public MgAnswerQuestionMatching AnswerQuestionMatching { get; set; }

        // use for checking data user submit
        public List<MgCorrectAnswerQuestionMatching> CorrectAnswerQuestionMatchings { get; set; }

        public List<MgAnswerDto> Answers { get; set; }
        public List<MgQuestionTranslationDto> Translations { get; set; }
        public long? AudioDuration { get; set; }
        public TimeSpan? RecordingTime { get; set; }
        public int? MaxWritingLength { get; set; }
        public TimeSpan? SpaceTime { get; set; }
        public string SampleTemplateJsonObject { get; set; }
        public Guid? AudioFileId { get; set; }
    }

    public class MgAnswerDto
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

        public DateTime? Created { get; set; }

        public DateTime? Modified { get; set; }
    }

    public class MgQuestionTranslationDto
    {
        public Guid QuestionId { get; set; }
        public string WordTranslation { get; set; }
        public string LanguageCode { get; set; }
    }

    public class MgMatchingJsonModel
    {
        public MgMatchingQuestionJson MatchingQuestion { get; set; }
        public MgMatchingAnswerJson Answer { get; set; }

    }

    public class MgAnswerQuestionMatching
    {
        public List<MgMatchingQuestionJson> Questions { get; set; }
        public List<MgMatchingAnswerJson> Answers { get; set; }
    }


    public class MgCorrectAnswerQuestionMatching
    {
        public Guid MatchingQuestionId { get; set; }
        public Guid AnswerId { get; set; }
    }

    public class MgMatchingQuestionJson
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid? ImageFileId { get; set; }

        public FileDto ImageFileInfo { get; set; }
    }

    public class MgMatchingAnswerJson
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid QuestionId { get; set; }
    }
}