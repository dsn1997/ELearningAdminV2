using IIG.Core.Common.Enums;

namespace IIG.Core.Common.ErrorHandling
{
    public static class ErrorMessage
    {
        public static string GetProductionMessage()
        {
            return "error_something_happened_key";
        }

        public static List<ErrorDto> Get(Exception exception, bool isProductionEnvironment = true)
        {
            List<ErrorDto> errors = new();
            if (exception == null)
            {
                return errors;
            }

            Exception ex = exception.GetBaseException();
            if (ex is ApiException apiException)
            {
                errors.Add(new ErrorDto
                {
                    Type = apiException.Type,
                    Message = apiException.Message,
                    Field = apiException.Field,
                    Key = apiException.Key
                });
            }
            else
            {
                errors.Add(new ErrorDto
                {
                    Type = EErrorApiType.Error,
                    Message = isProductionEnvironment ? GetProductionMessage() : ex.Message
                });
            }

            return errors;
        }
    }
}
