using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace IHunger.WebAPI.Extensions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        readonly ILogger<ExceptionMiddleware> _log;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> log)
        {
            _log = log;
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                _log.LogError(ex.Message);
                HandleExceptionAsync(httpContext, ex);
            }
        }

        private static void HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
        }
    }
}
