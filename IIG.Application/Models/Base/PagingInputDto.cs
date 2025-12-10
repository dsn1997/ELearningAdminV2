

using static IIG.Core.Common.ConfigureModels.Constants;

namespace IIG.Web.Data.Models
{
    public class ResponseData
    {
        public Code Code { get; set; } = Code.Success;
        public string Message { get; set; } = "Thành công";

        public ResponseData()
        {
        }

        public ResponseData(Code code, string message)
        {
            Code = code;
            Message = message;
        }
    }

    public class ResponseDataObject<T> : ResponseData
    {
        public T? Items { get; set; }
        public ResponseDataObject() : base() { }
        public ResponseDataObject(Code code, string message) : base(code, message) { }
        public ResponseDataObject(T data)
        {
            Items = data;
        }

        public ResponseDataObject(T data, Code code, string message) : base(code, message)
        {
            Items = data;
        }
    }
    public class ResponseDataFromAuthenService
    {
        public int StatusCode { get; set; } = (int)Code.Success;
        public string Message { get; set; } = "Success";

        public ResponseDataFromAuthenService()
        {
        }

        public ResponseDataFromAuthenService(int statusCode, string message)
        {
            StatusCode = statusCode;
            Message = message;
        }
    }
    public class ResponseDataObjectFromAuthenService<T> : ResponseDataFromAuthenService
    {
        public T? Data { get; set; }
        public ResponseDataObjectFromAuthenService() : base() { }
        public ResponseDataObjectFromAuthenService(int statusCode, string message) : base(statusCode, message) { }
        public ResponseDataObjectFromAuthenService(T data)
        {
            Data = data;
        }

        public ResponseDataObjectFromAuthenService(T data, int statusCode, string message) : base(statusCode, message)
        {
            Data = data;
        }
    }
    public class ResponseDataError : ResponseData
    {
        public List<string>? ErrorDetail { get; set; }

        public ResponseDataError(Code code, string message, List<string>? errorDetail = null) : base(code, message)
        {
            ErrorDetail = errorDetail;
        }
    }

    public class Pagination
    {
        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
        public string Order { get; set; }
        public string Filter { get; set; } = string.Empty;  
        public string LikeTextSearch
        {
            get
            {
                return $"%{Filter}%";
            }
        }

        public Pagination() { }
        public Pagination(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        
        }
    }

    public class PageableData<T> : ResponseDataObject<T>
    {

        public int PageSize { get; set; } = 20;
        public int PageNumber { get; set; } = 1;
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }

        public PageableData() : base() { }
        public PageableData(Code code, string message) : base(code, message) { }
        public PageableData(T data, Code code, string message) : base(code, message)
        {
            Items = data;
        }
        public PageableData(T data, Pagination pagination, Code code, string message) : base(data, code, message)
        {
            PageNumber = pagination.PageNumber;
            PageSize = pagination.PageSize;

        }

    }

    public enum Code
    {
        Success = 200,
        BadRequest = 400,
        Forbidden = 403,
        NotFound = 404,
        MethodNotAllowed = 405,
        ServerError = 500
    }
}
