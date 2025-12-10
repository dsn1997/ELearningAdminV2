using IIG.Core.Common.Enums;
using IIG.Core.Common.Models.Files;
using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public interface ISampleTemplateProperty
    {
        /// <summary>
        /// Lưu danh sách câu trả lời mẫu, nhận xét mẫu cho phần writing
        /// </summary>
        [MaxLength]
        string SampleTemplateJsonObject { get; set; }
    }

    /// <summary>
    /// Cấu trúc một item nằm trong SampleTemplateJsonObject
    /// </summary>
    public class SampleTemplateDto
    {
        public ESampleTemplateType Type { get; set; }
        public string Content { get; set; }
        public FileInsertModel FileInfo { get; set; }
        public string QuestionId { get; set; }
        public int? SortOrder { get; set; }
    }

    public class SampleTemplateViewModel 
            : IAudioFileDuration, IAudioFileProperty
    {
        public ESampleTemplateType Type { get; set; }
        public string Content { get; set; }
        public string QuestionId { get; set; }
        public int? SortOrder { get; set; }
        public long? AudioDuration { get; set; }
        public Guid? AudioFileId { get; set; }
    }
}
