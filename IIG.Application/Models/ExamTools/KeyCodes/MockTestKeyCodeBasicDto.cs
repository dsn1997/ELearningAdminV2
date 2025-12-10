using IIG.Core.Common.Enums;

namespace IIG.Application.Models.KeyCodes;

public class MockTestKeyCodeBasicDto
{
    public string Code { get; set; }

    public DateTime? SubmittedDate { get; set; }

    public Guid MockTestId { get; set; }

    public string MockTestName { get; set; }

    public EMockTestScoreType MockTestScoreType { get; set; }

    public string TextViewMore { get; set; }

    public string LinkUrlViewMore { get; set; }
}