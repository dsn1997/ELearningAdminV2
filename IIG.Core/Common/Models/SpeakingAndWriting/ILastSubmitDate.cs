namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public interface ILastSaveAnswerDate
    {
        /// <summary>
        /// Dùng để xác định câu đang làm dở khi chưa submit bài thi
        /// </summary>
        public DateTime? LastSaveDate { get; set; }
    }
}
