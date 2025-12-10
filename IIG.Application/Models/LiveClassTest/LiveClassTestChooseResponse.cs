using IIG.Web.Data.Models.Answers;

namespace IIG.Web.Data.Models.LiveClassTest;
public class LiveClassTestChooseResponse : ChooseBaseModelResponse
{
    public Guid LiveClassTestId { get; set; }

    public Guid WebUserId { get; set; }

    public Guid MockTestPartId { get; set; }

    public Guid MockTestSectionId { get; set; }
}
