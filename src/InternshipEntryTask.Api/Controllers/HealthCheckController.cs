using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace InternshipEntryTask.Api.Controllers;

[ApiController]
[ApiVersionNeutral]
public class HealthCheckController : ControllerBase
{
    [HttpGet("/health")]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IResult Check()
    {
        return Results.Ok();
    }
}
