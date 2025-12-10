using IIG.Application.Models;

namespace IIG.Application.Models;
public class LiveClassTestChooseResponse : ChooseBaseModelResponse
{
    public Guid LiveClassTestId { get; set; }

    public Guid WebUserId { get; set; }

    public Guid MockTestPartId { get; set; }

    public Guid MockTestSectionId { get; set; }
}
