using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace NetCoreAngular.Common
{
    public class Response
    {
        public bool Success { get; set; } = true;
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
    }

    public class Response<T> : Response
    {
        public T Content { get; set; }
        public List<string> ErrorMessages { get; set; } = new List<string>();
    }

}
