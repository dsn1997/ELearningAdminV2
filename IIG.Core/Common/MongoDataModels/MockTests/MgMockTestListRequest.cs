using IIG.Core.Common.Enums;
using IIG.Core.Providers.MongoDbProvider.Models;

namespace IIG.Core.Common.MongoDataModels.MockTests
{
    public class MgMockTestListRequest : MgBaseListRequest<EMockTestOrderableField>
    {
        public Guid? CoursePriceId { get; set; }
        public List<Guid> Ids { get; set; } = new List<Guid>();
        public Guid? MockTestTypeId { get; set; }
        public Guid? MockTestObjectId { get; set; }
        public List<Guid> IgnoreIds { get; set; } = new();
        public EMockTestStatus? Status { get; set; } = EMockTestStatus.Active;
    }
}
