using IIG.Core.Common.Models.MockTests;

namespace IIG.Web.Data.Models.Practices.UnitTest;
public class StartDoingUnitTestResponse
    : IRedoSettingProperty
{
    public string UnitTestTitle { get; set; }

    public List<Guid> QuestionnaireIds { get; set; }

    public List<UnitTestMenuListModel> Menu { get; set; }
    public int? RedoNumber { get; set; }

    public bool IsSWType { get; set; }


    public LastSavedUnittestQuestion LastSavedUnittestQuestion { get; set; }
}

public class LastSavedUnittestQuestion
{
    public Guid? QuestionnaireId { get; set; }
    public Guid? QuestionId { get; set; }
}
