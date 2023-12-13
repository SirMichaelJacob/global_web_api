using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace global_web_api.Middleware
{
    public class GlobalExceptionFilterAttribute : ExceptionFilterAttribute
    {
        private readonly ILogger<GlobalExceptionFilterAttribute> _logger;

        public GlobalExceptionFilterAttribute(ILogger<GlobalExceptionFilterAttribute> logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            _logger.LogError(context.Exception, "An unhandled exception occurred.");

            var error = new Error()
            {
                StatusCode = "500",
                Message = context.Exception.Message,
            };
            // Customize the response based on your requirements
            context.Result = new ObjectResult("Internal Server Error")
            {
                StatusCode = 500,
            };

            context.ExceptionHandled = true;
        }
    }
}
