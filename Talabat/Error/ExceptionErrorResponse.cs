﻿using System.Net;

namespace Talabat.Error
{
    public class ExceptionErrorResponse : ErrorResponse
    {
        public ExceptionErrorResponse(string? message = null, string? details = null) : 
            base(HttpStatusCode.InternalServerError, message)
        {
        }
        public override string? GetMessage(HttpStatusCode statusCode)
        {
            return "Some Error Ocured";
        }
    }
}
