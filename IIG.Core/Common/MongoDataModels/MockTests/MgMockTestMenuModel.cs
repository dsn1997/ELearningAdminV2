using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.SpeakingAndWriting;
using MongoDB.Bson.Serialization.Attributes;

namespace IIG.Core.Common.MongoDataModels.MockTests;
public class MgMockTestMenuModel
{
    public List<MgMenuMockTestSectionModel> Sections { get; set; } = new();
}
[BsonIgnoreExtraElements]
public class MgMenuMockTestSectionModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int SortOrder { get; set; }
    public int NumberOfTime { get; set; }
    public EMockTestSectionType Type { get; set; }
    public List<MgMenuMockTestPartModel> Parts { get; set; } = new();
}
[BsonIgnoreExtraElements]
public class MgMenuMockTestPartModel : ITotalPartTimeProperty
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public int SortOrder { get; set; }
    public int DelayTimeEachQuestion { get; set; }
    public List<MgMenuQuestionnaireModel> Questionnaires { get; set; } = new();
    public Guid? IntroAudioFileId { get; set; }
    public TimeSpan? TotalPartTime { get ; set ; }
}
[BsonIgnoreExtraElements]
public class MgMenuQuestionnaireModel
{
    public Guid Id { get; set; }
    public int SortOrder { get; set; }
    public List<MgMenuQuestionModel> Questions { get; set; } = new();
    public TimeSpan? TotalTime { get; set; }
}

public class MgMenuQuestionModel
{
    public Guid Id { get; set; }
    public int SortOrder { get; set; }
    public bool? Marked { get; set; }
    public EAnswerStatus Status { get; set; }
}