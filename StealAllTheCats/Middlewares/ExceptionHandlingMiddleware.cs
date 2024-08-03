using System.Net;

namespace StealAllTheCats.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate _next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            logger.LogError($"EXCEPTION: {exception.Message} INNER EXCEPTION: {exception.InnerException}");

            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            context.Response.ContentType = "application/json";
        }
    }
}