using ChannelEngine.Application.Exceptions;
using System.Net;

namespace ChannelEngine.MVC.Middlewares.ErrorHandling
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Exception middleware");
                await HandleException(context, ex);
            }
        }

        private static async Task HandleException(HttpContext httpContext, Exception exe)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = GetStatusCode(exe);

            var errorDetails = GetErrorDetails(exe);
            await httpContext.Response.WriteAsync(errorDetails.ToString());

        }

        private static int GetStatusCode(Exception exception)
        {
            int code = (int)HttpStatusCode.InternalServerError;
            if (exception is ChannelEngineException)
            {
                code = (int)HttpStatusCode.BadRequest;
            }
            return code;
        }

        private static ErrorDetails GetErrorDetails(Exception exe)
        {
            var errorDetails = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Error in handling the request. Sorry, please, try again a bit later!.",
            };

            if (exe is ChannelEngineException)
            {
                errorDetails.StatusCode = GetStatusCode(exe);
                errorDetails.Message = exe.Message;
            };

            return errorDetails;
        }
    }
}
