namespace IIG.Application.Models.MockTestPart;
public class MockTestPartDetailDto
{
    public Guid Id { get; set; }
    public Guid MocktestSectionId { get; set; }
    public string Name { get; set; }
    public string Content { get; set; }
    public int SortOrder { get; set; }
    public Guid? IntroAudioFileId { get; set; }
    public int DelayTimeEachQuestion { get; set; }
}
