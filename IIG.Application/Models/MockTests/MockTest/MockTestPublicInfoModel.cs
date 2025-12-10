using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.MockTests.MockTest;

public class MockTestPublicInfoModel
{
    public Guid Id { get; set; }
    public EMockTestStatus Status { get; set; }
    public DateTime? PublishedAt { get; set; }
}