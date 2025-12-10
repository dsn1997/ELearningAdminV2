using System.ComponentModel.DataAnnotations;

namespace IIG.Core.Common.Models.MockTests
{
    public interface IRedoSettingProperty
    {
        /// <summary>
        /// Số lần nhiều nhất có thể redo lại
        /// </summary>
        [MaxLength(5)]
        public int? RedoNumber { get; set; }
    }
}
