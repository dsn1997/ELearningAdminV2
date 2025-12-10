using IIG.Application.Models;

namespace IIG.Application.Models.WebUserUnitTestChoose;
public class WebUserUnitTestChooseInsert
{
    public Guid WebUserId { get; set; }

    public Guid UnitTestId { get; set; }

    public List<ChooseBaseModelResponse> ChooseBaseModelResponse { get; set; }
}
