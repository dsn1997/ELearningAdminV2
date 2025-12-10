namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public interface ITotalTimeProperty
    {
        /// <summary>
        /// Cấu hình tổng thời gian thi của một questionnaire
        /// </summary>
        TimeSpan? TotalTime { get; set; }
    }
}
