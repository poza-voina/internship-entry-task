using InternshipEntryTask.Abstractions.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net.Mime;

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
            Title = nameof(Results.InternalServerError),
            Detail = exception.Message,
            Status = StatusCodes.Status500InternalServerError,
        };

        if (exception is EntityNotFoundException)
        {
            problemDetails.Title = nameof(Results.NotFound);
            problemDetails.Status = StatusCodes.Status404NotFound;
        }
        else if (exception is ValidationException)
        {
            problemDetails.Title = nameof(Results.BadRequest);
            problemDetails.Status = StatusCodes.Status400BadRequest;
        }
        else if (exception is ConflictException)
        {
            problemDetails.Title = nameof(Results.Conflict);
            problemDetails.Status = StatusCodes.Status409Conflict;
        }
        else if (exception is UnprocessableException)
        {
            problemDetails.Title = nameof(Results.UnprocessableEntity);
            problemDetails.Status = StatusCodes.Status422UnprocessableEntity;
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