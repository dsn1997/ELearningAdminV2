namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public interface ITotalPartTimeProperty 
    {
        /// <summary>
        /// Tổng thời gian của part (đề thi)
        /// </summary>
        TimeSpan? TotalPartTime { get; set; }
        
        //double? DisplayTotalPartTime
        //{
        //    get
        //    {
        //        return TotalPartTime.HasValue ? TotalPartTime.Value.TotalMinutes : null;
        //    }
        //    set
        //    {
        //        DisplayTotalPartTime = value;
        //    }
        //}
    }
}
