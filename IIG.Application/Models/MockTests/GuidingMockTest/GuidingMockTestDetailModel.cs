namespace IIG.Application.Models.GuidingMockTest;
public class GuidingMockTestDetailModel
{
    public Guid Id { get; set; }

    public Guid MockTestTypeId { get; set; }

    public Guid MockTestObjectId { get; set; }

    public Guid CategoryId { get; set; }

    public string DesktopImageUrl { get; set; }

    public string MobileImageUrl { get; set; }

    public string Name { get; set; }

    public string Content { get; set; }
}
