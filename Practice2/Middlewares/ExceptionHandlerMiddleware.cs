using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Threading.Tasks;

namespace UPB.Practice2.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch(Exception ex)
            {
                await ProcessError(httpContext, ex);
            }
        }

        private Task ProcessError(HttpContext httpContext, Exception ex)
        {
            /*HttpStatusCode statusCode;

            switch (ex)
            {
                case ArgumentException _:
                    statusCode = HttpStatusCode.BadRequest;
                    break;
                case UnauthorizedAccessException _:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case NotImplementedException _:
                    statusCode = HttpStatusCode.NotImplemented;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }*/

            string errorBodyJSON = $"{{\r\n Message = {ex.Message}\r\n}}";

            return httpContext.Response.WriteAsync(errorBodyJSON);
        }
    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionHandlerMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionHandlerMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
