using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InternshipEntryTask.Api.Controllers;


/// <summary>
/// Контроллер для обработки здоровья сервиса
/// </summary>
[ApiController]
[ApiVersionNeutral]
public class HealthCheckController : ControllerBase
{

    /// <summary>
    /// Обработывает здоровье API
    /// </summary>
    /// <returns>200</returns>
    [HttpGet("/health")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IResult Check()
    {
        return Results.Ok();
    }
}
