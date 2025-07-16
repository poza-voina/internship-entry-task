using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Core.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace InternshipEntryTask.Api.Controllers.v1;

/// <summary>
/// Контроллер для обработки запросов игры
/// </summary>
/// <param name="gameService"><inheritdoc cref="IGameService"/></param>
/// <param name="etagService"><inheritdoc cref="IETagService"/></param>
[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/games")]
public class GameController(IGameService gameService, IETagService etagService) : ControllerBase
{
    /// <summary>
    /// Получает игру по идентификатору
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="showBoard">Флаг определяющий, следует ли отображать игровую доску</param>
    /// <returns>Объект игры</returns>
    [HttpGet("{gameId:long}")]
    [ProducesResponseType(typeof(GameDto), (int)HttpStatusCode.OK)]
    public async Task<IResult> GetGame(
        [FromRoute] long gameId,
        [FromHeader(Name = "Show-Board")] bool showBoard = false)
    {
        GameDto result = await gameService.GetGameAsync(gameId, showBoard);

        return Results.Ok(result);
    }

    /// <summary>
    /// Создает игру
    /// </summary>
    /// <returns>Объект игры</returns>
    [HttpPost]
    [ProducesResponseType(typeof(GameDto), (int)HttpStatusCode.OK)]
    public async Task<IResult> CreateGame()
    {
        GameDto result = await gameService.CreateGameAsync();

        return Results.Ok(result);
    }

    /// <summary>
    /// Присоединиться к игре
    /// </summary>
    /// <param name="request">Запрос на присоединени</param>
    /// <param name="joinKey">Ключ, используемый для присоедининения к игре</param>
    /// <returns>Объект игры</returns>
    [HttpPost("join")]
    [ProducesResponseType(typeof(GameDto), (int)HttpStatusCode.OK)]
    public async Task<IResult> JoinGame(
        [FromBody] JoinRequest request,
        [FromHeader(Name = "X-Join-Key")] [Required] Guid joinKey)
    {
        var result = await gameService.JoinGameAsync(request, joinKey);

        return Results.Ok(result);
    }

    /// <summary>
    /// Сделать ход
    /// </summary>
    /// <param name="moveRequest">Запрос на создание хода</param>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="accessKey">Ключ авторизации</param>
    /// <param name="showBoard">Флаг определяющий, следует ли отображать игровую доску</param>
    /// <returns>Объект игр</returns>
    [HttpPost("{gameId:long}/move")]
    [ProducesResponseType(typeof(GameDto), (int)HttpStatusCode.OK)]
    public async Task<IResult> Move(
        [FromBody] MoveRequest moveRequest,
        [FromRoute] long gameId,
        [FromHeader(Name = "X-Access-Key")] [Required] Guid accessKey,
        [FromHeader(Name = "Show-Board")]
        bool showBoard = false)
    {
        if (etagService.Check(gameId, accessKey, moveRequest, out var etag))
        {
            Response.Headers.ETag = etag;
            return Results.Ok();
        }

        var result = await gameService.MoveAsync(gameId, moveRequest, accessKey, showBoard);

        Response.Headers.ETag = etagService.GetETag(gameId, accessKey);
        return Results.Ok(result);
    }
}