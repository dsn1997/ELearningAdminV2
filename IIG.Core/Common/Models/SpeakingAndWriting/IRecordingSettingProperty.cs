namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public interface IRecordingSettingProperty
    {
        /// <summary>
        /// Cấu hình thời gian ghi âm tối đa
        /// </summary>
        TimeSpan? RecordingTime { get; set; }
    }
}
