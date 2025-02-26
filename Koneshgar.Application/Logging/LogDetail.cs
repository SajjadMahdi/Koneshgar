using System;
using System.Collections.Generic;
using System.Text;

namespace Koneshgar.Application.Logging
{
    public class LogDetail
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string ManagerName { get; set; }
        public string MethodName { get; set; }
        public object Data { get; set; }
    }
}
