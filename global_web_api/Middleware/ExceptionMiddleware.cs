using System.Net;

namespace global_web_api.Middleware
{
    // You may need to install the Microsoft.AspNetCore.Http.Abstractions package into your project
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {

            try
            {
                await _next(httpContext);
            }
            catch (System.Exception e)
            {

                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var error = new Error
                {
                    StatusCode = "Error Code: " + e.Message,
                    Message = "There was an Error: ",
                };

                await httpContext.Response.WriteAsync(error.ToString());
            }

        }


    }

    // Extension method used to add the middleware to the HTTP request pipeline.
    public static class ExceptionMiddlewareExtensions
    {
        public static IApplicationBuilder UseExceptionMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
