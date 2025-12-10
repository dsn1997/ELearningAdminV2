using IIG.Core.Common.Enums;

namespace IIG.Web.Data.Models.ExamTools.KeyCodes;

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