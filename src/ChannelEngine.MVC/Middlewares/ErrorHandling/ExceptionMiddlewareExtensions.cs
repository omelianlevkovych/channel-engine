namespace ChannelEngine.MVC.Middlewares.ErrorHandling
{
    public static class ExceptionMiddlewareExtensions
    {
        public static void UseCustomExceptions(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
