using IIG.Core.Common.Enums;

namespace IIG.Application.Models;
public class MockTestLiveClassTestBasicDto
{
    public Guid LiveClasTestId { get; set; }

    public Guid WebUserId { get; set; }

    public Guid MockTestId { get; set; }

    public EMockTestScoreType MockTestScoreType { get; set; }
}
