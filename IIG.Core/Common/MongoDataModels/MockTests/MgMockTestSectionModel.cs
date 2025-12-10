using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using MongoDB.Bson.Serialization.Attributes;

namespace IIG.Core.Common.MongoDataModels.MockTests
{
    public class MgMockTestSectionModel
    {
        public Guid Id { get; set; }
        public Guid MockTestId { get; set; }
        public Guid RankingScoreId { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public EMockTestSectionType Type { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfTime { get; set; }
        public int? NumberOfTimeEstimate { get; set; }
        public List<MgMockTestPartModel> Parts { get; set; } = new List<MgMockTestPartModel>();
    }

    public class MgMockTestPartModel : ITotalPartTimeProperty
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int SortOrder { get; set; }
        public int NumOfQuestions { get; set; }
        public int NumOfTime { get; set; }
        public string Content { get; set; }
        public Guid? IntroAudioFileId { get; set; }
        public int DelayTimeEachQuestion { get; set; }

        public List<MgMockTestQuestionnaireInfo> Questionnaires { get; set; } = new List<MgMockTestQuestionnaireInfo>();
        public TimeSpan? TotalPartTime { get; set; }
    }
    [BsonIgnoreExtraElements]
    public class MgMockTestQuestionnaireInfo
    {
        public Guid Id { get; set; }
        public string Identifier { get; set; }
        public string Name { get; set; }
        public EQuestionnaireType Type { get; set; }
        public int NumberOfQuestions { get; set; }
        public int NumberOfTime { get; set; }
        public bool IsActive { get; set; } = true;
        public TimeSpan? TotalTime { get; set; }
    }

    public class OverloadCodeTest
    {
        public int UserLive { get; set; }
        public int UserOverload { get; set; }
    }
}
