using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Utility.Exceptions;
using Utility.Models;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionMiddleware(RequestDelegate next, IWebHostEnvironment env)
        {
            _next = next;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                await HandleExceptionAsync(context, ex, _env);
            }           

        }

        private async Task HandleExceptionAsync(HttpContext context, Exception ex, IWebHostEnvironment env)
        {
            var code = HttpStatusCode.InternalServerError;
            var error = new ApiErrorResponse
            {
                StatusCode = (int)code
            };

            if(_env.IsDevelopment())
            {
                error.Details = ex.StackTrace;
            }
            else
            {
                error.Details = ex.Message;
            }

            switch(ex)
            {
                case ApplicationValidationException e:
                    error.Message = e.Message;
                    error.StatusCode = (int)HttpStatusCode.UnprocessableEntity;
                    break;
                default:
                    error.Message = "Something Went Wrong";
                    break;
            }
            var result = JsonConvert.SerializeObject(error);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = error.StatusCode;
            await context.Response.WriteAsync(result);
        }
    }
}
