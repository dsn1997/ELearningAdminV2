using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    public interface IMaxWritingLengthProperty
    {
        /// <summary>
        /// Dạng writing - tab câu hỏi và trả lời - số từ viết tối đa
        /// </summary>
        
        [MaxLength(4)]        
        int? MaxWritingLength { get; set; }
    }
}
