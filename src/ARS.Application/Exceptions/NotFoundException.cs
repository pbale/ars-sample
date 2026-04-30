using System.Security.Cryptography.X509Certificates;
using ARS.Application.DTO.Error;
namespace ARS.Application.Exceptions;

public class NotFoundException : Exception    {
        public int StatusCode { get; set; } = 404;
        public ErrorDTO ErrorDetails { get; set; }
        public NotFoundException(int code, string message)
            : base(message)
        {
            ErrorDetails = new ErrorDTO(code, message);
        }
}