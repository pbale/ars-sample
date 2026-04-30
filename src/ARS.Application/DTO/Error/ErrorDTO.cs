namespace ARS.Application.DTO.Error;

public class ErrorDTO
{
    public int StatusCode { get; set; } = 0;
    public string Message { get; set; } = string.Empty;
    public string? CorrelationId { get; set; }

    public List<string>? Details { get; set; }

    public ErrorDTO(int statusCode, string message, string? correlationId = null, List<string>? details = null)
    {
        StatusCode = statusCode;
        Message = message;
        CorrelationId = correlationId;
        Details = details;
    }
}   

