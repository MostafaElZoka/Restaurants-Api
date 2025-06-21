using Restaurant.Domain.Exceptions;

namespace Restaurant.MiddleWares
{
    public class ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger) : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context /*represents the incoming request*/, RequestDelegate next)
        {
            try
            {
                await next.Invoke(context);
            }
            catch(NotFoundExceptionHandler notfound)
            {
                logger.LogWarning("not found exception");
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync(notfound.Message);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, ex.Message);
                context.Response.StatusCode = 500;
                await context.Response.WriteAsync("something went wrong");
            }
        }
    }

}
