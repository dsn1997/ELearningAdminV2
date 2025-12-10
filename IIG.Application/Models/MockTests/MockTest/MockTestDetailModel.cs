using IIG.Core.Common.Enums;

namespace IIG.Application.Models.MockTest;

public class MockTestDetailModel
{
    public Guid Id { get; set; }
    public List<MockTestTranslationDto> Translations { get; set; }
    public Guid MockTestTypeId { get; set; }
    public Guid MockTestObjectId { get; set; }
    public EMockTestStatus Status { get; set; }
    public EMockTestScoreType ScoreType { get; set; }
    public string Tags { get; set; }
    public DateTime Created { get; set; }
}

public class Menu_MockTestDetailDto
{
    public Guid Id { get; set; }
    public int? Questions { get; set; }
    public int? Time { get; set; }
    public int? NumberTests { get; set; }
    public IEnumerable<string> Sections { get; set; }

}
