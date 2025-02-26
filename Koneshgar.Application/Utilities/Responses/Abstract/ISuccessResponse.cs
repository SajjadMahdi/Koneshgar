using System;
using System.Collections.Generic;
using System.Text;

namespace Koneshgar.Application.Utilities.Responses.Abstract
{
    public interface ISuccessResponse : IResponse
    {
        string Message { get; }
    }
}
