using IIG.Core.Common.Enums;
using MongoDB.Bson.Serialization.Attributes;

namespace IIG.Core.Common.MongoDataModels.MockTests
{
    [BsonIgnoreExtraElements]
    public class MgMockTestInfoModel
    {
        public Guid Id { get; set; }
        public List<MgNameTranslationModel> Names { get; set; } = new List<MgNameTranslationModel>();
        public MgMockTestTypeModel MockTestType { get; set; }
        public MgMockTestObjectModel MockTestObject { get; set; }
        public EMockTestStatus Status { get; set; }
        public EMockTestScoreType ScoreType { get; set; }
        public string[] Tags { get; set; }
        public DateTime Created { get; set; }
        public DateTime PublishedAt { get; set; }
    }

    public class MgNameTranslationModel
    {
        public string Name { get; set; }
        public string LanguageCode { get; set; }
    }

    public class MgMockTestTypeModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class MgMockTestObjectModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
