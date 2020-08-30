using System;
using System.Collections.Generic;
using System.Text;

namespace DLL.Wrapper
{
    public class Response<T>
    {
        public Response()
        {
        }
        public Response(T data)
        {
            IsSuccess = true;
            Message = string.Empty;
            Errors = null;
            Data = data;
        }
        public T Data { get; set; }
        public bool IsSuccess { get; set; }
        public string[] Errors { get; set; }
        public string Message { get; set; }
    }
}
