using System;
using System.Net;
using LegendaryDashboard.Domain.Exceptions;
using Microsoft.IdentityModel.Tokens;

namespace LegendaryDashboard.Api
{
    public static class ExceptionExtensions
    {
        public static (int Code, string Error) ToHttpResponse(this Exception exception)
        {
            return (GetHttpCode(exception), GetHttpError(exception));
        }
 
        public static int GetHttpCode(this Exception exception)
        {
            return exception switch
            {
                UserReadableException _ => (int) HttpStatusCode.BadRequest,
 
                UnauthorizedAccessException _ => (int) HttpStatusCode.Unauthorized,
 
                _ => (int) HttpStatusCode.InternalServerError
            };
        }
 
        public static string GetHttpError(this Exception exception)
        {
            return exception switch
            {
                UserReadableException _ => exception.Message,
 
                UnauthorizedAccessException _ => "Недостаточно прав для выполнения запроса.",

                _ => "Произошла внутренняя ошибка сервера.",
            };
        }
    }
}