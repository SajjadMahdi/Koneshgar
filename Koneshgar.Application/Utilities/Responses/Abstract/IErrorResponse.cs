using System;
using System.Collections.Generic;
using System.Text;

namespace Koneshgar.Application.Utilities.Responses.Abstract
{
    public interface IErrorResponse : IResponse
    {
       List<string> Errors { get; }
    }
}
