
namespace Service.Helper
{
    public class CommonResponse
    {
        public CommonResponse()
        { }

        public CommonResponse(int statusCode, object? result = null, string? message = null)
        {
            StatusCode = statusCode;
            Result = result;
            Message = message;
        }
        public int StatusCode { get; set; }
        public object? Result { get; set; }
        public string? Message { get; set; }

        public static CommonResponse Success(object? result = null, string? message = null)
        {
            return new CommonResponse(200, result, message);
        }
        public CommonResponse Error(int statusCode, string? message = null, object? result = null)
        {
            return new CommonResponse(statusCode, result, message);
        }
    }
}