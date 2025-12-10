using IIG.Core.Providers.MongoDbProvider.Infrastructure;
using IIG.Core.Providers.MongoDbProvider.Models;
using MongoDB.Bson.Serialization.Attributes;

namespace IIG.Core.Common.MongoDataModels.MockTests
{
    [BsonCollection("mocktests")]
    [BsonIgnoreExtraElements]

    public class MgMockTestModel : Document
    {
        public Guid MockTestId { get; set; }
        public MgMockTestInfoModel GeneralInfo { get; set; } = new MgMockTestInfoModel();
        public List<MgMockTestSectionModel> MockTestSections { get; set; } = new List<MgMockTestSectionModel>();
        public MgMockTestMenuModel MockTestMenu { get; set; } = new MgMockTestMenuModel();
    }
}
