using Serilog.Context;

namespace ARS.API.Middleware;

public class CorrelationIdMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        string correlationId =
            context.Request.Headers["X-Correlation-ID"].FirstOrDefault()
            ?? GenerateCorrelationId(context);

        context.Response.Headers["X-Correlation-ID"] = correlationId;

        // Push the correlation ID into the Serilog context so that it is included in all log entries for this request
        using (LogContext.PushProperty("CorrelationId", correlationId))
        {
            await next(context);
        }
    }

    private static string GenerateCorrelationId(HttpContext context)
    {
        //var timestamp = DateTime.UtcNow.ToString("yyyyMMdd-HHmmss");
        string endpoint = context.Request.Path.Value?.Replace("/", "-").Trim('-') ?? "API";
        string random = Guid.NewGuid().ToString("N")[..4];

        //Add correationId to HttpContext.Items for later retrieval in controllers or other middleware
        context.Items["CorrelationId"] = $"ARS-{endpoint}-{random}";

        //return $"ARS-{endpoint}-{timestamp}-{random}";
        return $"ARS-{endpoint}-{random}";
    }
}