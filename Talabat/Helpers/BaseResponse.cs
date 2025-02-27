using System.Net;

namespace Talabat.Helpers
{
    public class BaseResponse
    {
        public string? Message { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public BaseResponse(HttpStatusCode statusCode, string? message = null)
        {
            Message = message ?? string.Empty;
            StatusCode = statusCode;
        }
        public virtual string? GetMessage(HttpStatusCode statusCode)
        {
            return statusCode.ToString();
        }
    }
}
