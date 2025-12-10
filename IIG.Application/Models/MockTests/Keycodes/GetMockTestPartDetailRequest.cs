using IIG.Core.Common.MongoDataModels.Keycodes;

namespace IIG.Web.Data.Models.MockTests.Keycodes;
public class GetMockTestPartDetailRequest : MockTestKeyCodeBaseRequest
{
    public Guid MockTestPartId { get; set; }
}
