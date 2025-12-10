using IIG.Web.Data.Models.Answers;

namespace IIG.Web.Data.Models.Practices.WebUserUnitTestChoose;
public class WebUserUnitTestChooseInsert
{
    public Guid WebUserId { get; set; }

    public Guid UnitTestId { get; set; }

    public List<ChooseBaseModelResponse> ChooseBaseModelResponse { get; set; }
}
