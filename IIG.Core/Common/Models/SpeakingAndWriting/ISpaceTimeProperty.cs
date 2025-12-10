namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    /// <summary>
    /// Left section - Speaking and Writing
    /// </summary>
    public interface ISpaceTimeProperty
    {
        /// <summary>
        /// Thời gian trống, tg nghỉ
        /// </summary>
        TimeSpan? SpaceTime { get; set; }  
    }
}
