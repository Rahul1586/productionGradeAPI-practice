using System;
using System.Collections.Generic;
using System.Text;

namespace Utility.Models
{
    public class ApiErrorResponse
    {
        public string Message { get; set; }
        public string Details { get; set; }
        public bool IsSuccess { get; set; }
        public int StatusCode { get; set; }
    }
}
