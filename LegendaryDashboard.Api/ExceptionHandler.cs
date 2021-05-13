using System;
using System.Threading.Tasks;
using LegendaryDashboard.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace LegendaryDashboard.Api
{
    public sealed class ExceptionHandler
    {
        private readonly RequestDelegate _next;

        public ExceptionHandler(RequestDelegate next)
        {
            _next = next;
        }
 
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                
                await HandleExceptionAsync(context, ex);
                
            }
        }
 
        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var (code, error) = exception.ToHttpResponse();
 
            context.Response.StatusCode = code;
            context.Response.ContentType = "application/json";
 
            await context.Response.WriteAsync(error);
        }
    }
}