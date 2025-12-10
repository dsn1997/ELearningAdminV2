using IIG.Core.Common.Enums;

namespace IIG.Core.Common.ErrorHandling
{
    public abstract class ApiException : Exception
    {
        public EErrorApiType Type { get; } = EErrorApiType.Error;

        public string Field { get; }
        public string Key { get; }

        protected ApiException(EErrorApiType type, string message) : base(message)
        {
            Type = type;
        }

        protected ApiException(EErrorApiType type, string message, string field) : this(type, message)
        {
            Field = field;
        }

        protected ApiException(string message) : base(message)
        {
        }

        protected ApiException(string message, Exception innerException) : base(message, innerException)
        {
        }
        protected ApiException(EErrorApiType type, string message, string field, string key) : this(type, message, field)
        {
            Key = key;
        }
    }

    public class ApiValidationException : ApiException
    {
        public ApiValidationException(string message, string field) : base(EErrorApiType.Validation, message, field)
        {
        }
        public ApiValidationException(string message, string field, string key) : base(EErrorApiType.Validation, message, field, key)
        {
        }
    }

    public class ApiNotFoundException : ApiException
    {
        public ApiNotFoundException(string message) : base(message)
        {
        }

        public ApiNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class ApiSystemException : ApiException
    {
        public ApiSystemException(string message) : base(message)
        {
        }

        public ApiSystemException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }


    public class ApiForbiddenException : ApiException
    {
        public ApiForbiddenException(string message) : base(message)
        {
        }
    }

    public class ApiTooManyRequestsException : ApiException
    {
        public ApiTooManyRequestsException(string message) : base(message)
        {
        }
    }
}
