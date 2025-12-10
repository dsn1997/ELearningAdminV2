using IIG.Core.Common.Enums;

namespace IIG.Core.Common.ErrorHandling
{
    public class ErrorDto
    {
        public EErrorApiType Type { get; set; }

        public string Key { get; set; }

        public string Field { get; set; }

        public string Message { get; set; }
    }

    public class ErrorFromIdentityDto
    {
        public string TraceId { get; set; }
        public ICollection<ErrorDto> Errors { get; set; }
    }
}
