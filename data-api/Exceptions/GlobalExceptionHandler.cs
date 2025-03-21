using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using data_api.Exceptions;

namespace data_api.Exceptions;
public class GlobalExceptionHandler : IExceptionHandler
{
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
    {
        _logger = logger;
    }

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        var (statusCode, message, isValid) = GetExceptionDetails(exception);

        _logger.LogError(exception, exception.Message);

        httpContext.Response.StatusCode = (int)statusCode;
        var response = new
        {
            Message = message,
            IsValid = isValid
        };
        await httpContext.Response.WriteAsJsonAsync(response, cancellationToken);
        
        return isValid;
    }

    private (HttpStatusCode statusCode, string message, bool isValid) GetExceptionDetails(Exception exception)
    {
        return exception switch
        {
            LoginFailedException => (HttpStatusCode.Unauthorized, exception.Message, false),
            RefreshTokenException => (HttpStatusCode.BadRequest, exception.Message, false),
            TokenInvalidException => (HttpStatusCode.Unauthorized, exception.Message, false),
            _=> (HttpStatusCode.InternalServerError, $"An unexpected error occured: {exception.Message}", false)
        };
    }
}