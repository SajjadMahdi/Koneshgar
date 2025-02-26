using System;
using System.Collections.Generic;
using System.Text;

namespace Koneshgar.Application.Utilities.Responses.Abstract
{
    public interface IResponse
    {
         bool Success { get; }
         int StatusCode { get; }
    }
}
