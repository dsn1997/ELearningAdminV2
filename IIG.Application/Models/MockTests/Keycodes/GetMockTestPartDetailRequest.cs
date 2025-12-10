using IIG.Core.Common.MongoDataModels.Keycodes;

namespace IIG.Application.Models.Keycodes;
public class GetMockTestPartDetailRequest : MockTestKeyCodeBaseRequest
{
    public Guid MockTestPartId { get; set; }
}
