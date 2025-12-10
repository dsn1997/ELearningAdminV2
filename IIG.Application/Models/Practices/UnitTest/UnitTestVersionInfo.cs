using IIG.Web.Data.Models.Practices.Versioning;
using IIG.Web.Data.Models.Practices.WebUserUnitTestChoose;

namespace IIG.Web.Data.Models.Practices.UnitTest;

public class UnitTestVersionInfo
{
    public QuestionnaireVersionInfo QuestionnaireInfo { get; set; }
    
    public IEnumerable<WebUserUnitTestChooseModel> UnitTestSubmittedData { get; set; }
}