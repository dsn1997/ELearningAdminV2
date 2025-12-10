using IIG.Core.Common.Models.MockTests;

namespace IIG.Web.Data.Models.MockTests.PolicyMockTestSetting;
public class PolicyMockTestSettingDetailModel
    : IMocktestSettingRecordingTestDto
{
    public Guid Id { get; set; }

    public Guid SoundTestFileId { get; set; }

    public string PolicyName { get; set; }

    public string PolicyContent { get; set; }

    public string GuidingName { get; set; }

    public string GuidingContent { get; set; }

    public string SoundName { get; set; }

    public string SoundContent { get; set; }

    public string RecordingTitle { get; set; }
    public string RecordingContent { get; set; }
    public TimeSpan? MaxRecordingTime { get; set; }
}
