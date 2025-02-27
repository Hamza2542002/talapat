using System.Net;

namespace Talabat.Error
{
    public class ValidationErrorResponse : ErrorResponse
    {
        public ValidationErrorResponse(HttpStatusCode statusCode, string? message = null) 
            : base(HttpStatusCode.BadRequest, message)
        {
        }

        public ICollection<string>? Errors { get; set; }
        public override string? GetMessage(HttpStatusCode statusCode)
        {
            return "Error While Validating Data";
        }
    }
}
