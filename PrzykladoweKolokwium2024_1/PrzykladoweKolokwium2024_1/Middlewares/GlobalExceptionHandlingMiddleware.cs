using System.Text.Json;
using PrzykladoweKolokwium2024_1.Repositories;

namespace PrzykladoweKolokwium2024_1.Middlewares;

public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
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
            _logger.LogError(ex,"Unhandled exception occurred");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var response = new
        {
            status = "Error",
            message = "An unexpected error occurred.",
            detailedMessage = exception.Message,
            stackTrace = exception.StackTrace
        };
        
        var options = new JsonSerializerOptions{PropertyNamingPolicy = JsonNamingPolicy.CamelCase};
        var json = JsonSerializer.Serialize(response, options);
        
        await context.Response.WriteAsync(json);
    }
}


public static class GlobalExceptionHandlingMiddlewareExtensions
{
    public static IApplicationBuilder UseGlobalExceptionHandling(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandlingMiddleware>();
    }
}