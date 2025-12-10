using IIG.Core.Common.Enums;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace IIG.Core.Common.ErrorHandling
{
    public class BaseErrorDto
    {
        public string TraceId { get; set; }
        public List<ErrorDto> Errors { get; set; }

        public BaseErrorDto() { }
        public BaseErrorDto(ModelStateDictionary modelState, string traceId)
        {
            TraceId = traceId;
            Errors = modelState.Keys
                .SelectMany(key => modelState[key].Errors.Select(x => new ErrorDto
                {
                    Field = key,
                    Message = x.ErrorMessage,
                    Type = EErrorApiType.Validation
                }))
                .ToList();
        }

    }
}
