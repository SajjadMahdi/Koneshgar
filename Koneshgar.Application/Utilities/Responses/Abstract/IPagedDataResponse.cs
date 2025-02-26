using System;
using System.Collections.Generic;
using System.Text;

namespace Koneshgar.Application.Utilities.Responses.Abstract
{
    interface IPagedDataResponse<T> : IResponse
    {
         int TotalItems { get; }
         T Data { get; }
    }
}
