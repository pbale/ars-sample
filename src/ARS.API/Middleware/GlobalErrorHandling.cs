namespace ARS.API.Middleware;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;
using ARS.Application.Exceptions;
using ARS.Application.DTO.Error;

public class GlobalErrorHandlingMiddleware(RequestDelegate next, ILogger<GlobalErrorHandlingMiddleware> logger)
{
    private readonly RequestDelegate next = next;
    private readonly ILogger<GlobalErrorHandlingMiddleware> _logger = logger;
    public async Task Invoke(HttpContext context)
    {
        string correlationId = context.Items["CorrelationId"] as string ?? "N/A";

        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
             await HandleExceptionAsync(context, ex, correlationId);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception, string correlationId)
    {
        HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
        string message = "An unexpected error occurred.";
        List<string> errors = new();
        switch (exception)
        {
            case NotFoundException notFoundException:
                statusCode = (HttpStatusCode)notFoundException.StatusCode;
                //errors = notFoundException.Details ?? new List<string>();
                message = notFoundException.Message;
                
                logger.LogError("Not found. CorrelationId: {CorrelationId}", correlationId);

                break;
            // Handle other specific exceptions here as needed
        }

        var errorResponse = new ErrorDTO((int)statusCode, message, correlationId, errors);
        var jsonResponse = JsonSerializer.Serialize(errorResponse);

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        return context.Response.WriteAsync(jsonResponse);
    }
}   