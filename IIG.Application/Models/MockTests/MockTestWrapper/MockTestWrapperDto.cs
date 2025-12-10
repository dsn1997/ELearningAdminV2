using IIG.Application.Models;
using IIG.Application.Models.MockTest;
using System.ComponentModel.DataAnnotations;

namespace IIG.Web.Data.Models;
public class Menu_MockTestWrapperDto
{
    public Guid? Id { get; set; }
    public string Name { get; set; }
    public bool? IsDailyChallenge { get; set; }
    public DateTime? DailyChallengeEndDate { get; set; }
    public Menu_MockTestDetailDto MockTest { get; set; }
    public string ImageFileUrl { get; set; }
    public bool IsTested  { get; set; }
}
public class Menu_MockTestWrapperGroupChooseDto
{
    public Guid MockTestGroupId { get; set; }
    public string Name { get; set; }
}

public class Menu_MockTestWrapperPagingInputDto : Pagination
{
    public short? Type { get; set; }
    public Guid WrapperGroupId { get; set; }
}

public class MockTestWrapperDetailDto
{
    public Guid Id { get; set; }
    public string Instructions { get; set; }
    public string Descriptions { get; set; }
    public Guid MockTestId { get; set; }
    public MockTestStructureWithQuestionTagModel MockTestStructure { get; set; }
}
