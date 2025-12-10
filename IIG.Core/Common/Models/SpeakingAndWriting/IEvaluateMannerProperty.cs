using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Common.Models.SpeakingAndWriting
{
    /// <summary>
    /// Left section - writing,..
    /// </summary>
    public interface IEvaluateMannerProperty
    {
        /// <summary>
        /// Tiêu chí chấm điểm
        /// </summary>
        [MaxLength(500)]
        public string EvaluateManner { get; set; }
    }
}
