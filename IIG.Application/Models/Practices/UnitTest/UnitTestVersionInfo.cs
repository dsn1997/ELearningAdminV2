using IIG.Application.Models.Versioning;
using IIG.Application.Models.WebUserUnitTestChoose;

namespace IIG.Application.Models.UnitTest;

public class UnitTestVersionInfo
{
    public QuestionnaireVersionInfo QuestionnaireInfo { get; set; }
    
    public IEnumerable<WebUserUnitTestChooseModel> UnitTestSubmittedData { get; set; }
}