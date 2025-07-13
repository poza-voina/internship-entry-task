using InternshipEntryTask.Abstractions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace InternshipEntryTask.Api;

/// <summary>
/// Обработчик исключений
/// </summary>
/// <param name="problemDetailsService"><inheritdoc cref="IProblemDetailsService" path="/summary/node()"/></param>
public class ExceptionHandler(IProblemDetailsService problemDetailsService) : IExceptionHandler
{
    /// <inheritdoc/>
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var problemDetails = new ProblemDetails()
        {
            Title = exception.Message,
            Detail = exception.StackTrace,
        };

        if (exception is EntityNotFoundException)
        {
            problemDetails.Type = nameof(Results.NotFound);
            problemDetails.Status = StatusCodes.Status404NotFound;
        }
        else if (exception is BaseGameException)
        {
            problemDetails.Type = nameof(Results.BadRequest);
            problemDetails.Status = StatusCodes.Status400BadRequest;
        }
        else
        {
            problemDetails.Type = nameof(Results.InternalServerError);
            problemDetails.Status = StatusCodes.Status500InternalServerError;
        }

        if (problemDetails is { Status: not null })
        {
            httpContext.Response.StatusCode = problemDetails.Status.Value;
            return await problemDetailsService.TryWriteAsync(new()
            {
                HttpContext = httpContext,
                ProblemDetails = problemDetails,
            });
        }
        else
        {
            return true;
        }
    }
}