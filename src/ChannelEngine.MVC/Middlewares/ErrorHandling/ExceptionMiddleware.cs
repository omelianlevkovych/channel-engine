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

        private static async Task HandleException(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json";
            httpContext.Response.StatusCode = GetStatusCode(ex);

            var errorDetails = GetErrorDetails(ex);
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

        private static ErrorDetails GetErrorDetails(Exception ex)
        {
            var errorDetails = new ErrorDetails
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
                Message = "Error in handling the request. Sorry, please, try again a bit later!.",
            };

            if (ex is ChannelEngineException)
            {
                errorDetails.StatusCode = GetStatusCode(ex);
                errorDetails.Message = ex.Message;
            };

            return errorDetails;
        }
    }
}
