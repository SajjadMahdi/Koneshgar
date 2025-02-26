using Koneshgar.Application.Utilities.Responses.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Koneshgar.Application.Utilities.Responses.Concrete
{
    public class SuccessResponse : ISuccessResponse
    {
        public bool Success { get; } = true;
        public string Message { get; }
        public int StatusCode { get; }


        public SuccessResponse()
        {

        }

        public SuccessResponse(int statuscode, string message)
        {
            StatusCode = statuscode;
            Message = message;
        }
    }
}
