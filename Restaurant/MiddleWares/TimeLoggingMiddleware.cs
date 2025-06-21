
using System.Diagnostics;

namespace Restaurant.MiddleWares;

public class TimeLoggingMiddleware(ILogger<TimeLoggingMiddleware> logger) : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        var StopWatch = new Stopwatch();
        StopWatch.Start();
        await next.Invoke(context);
        StopWatch.Stop();
        if (StopWatch.ElapsedMilliseconds > 4000)
        {
            logger.LogInformation($"the Request with the verb: {context.Request.Method} and path : {context.Request.Path} took {StopWatch.ElapsedMilliseconds/1000}seconds");
        }

    }
}
