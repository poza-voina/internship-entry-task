
using InternshipEntryTask.Core.Data.Game;

namespace InternshipEntryTask.Core.Services.Interfaces;

/// <summary>
/// Сервис для работы с игрой
/// </summary>
public interface IGameService
{
    /// <summary>
    /// Создает игру
    /// </summary>
    /// <returns><inheritdoc cref="GameDto"/></returns>
    Task<GameDto> CreateGameAsync();

    /// <summary>
    /// Получает игру
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="showBoard">Флаг показа доски</param>
    /// <returns><inheritdoc cref="GameDto"/></returns>
    Task<GameDto> GetGameAsync(long gameId, bool showBoard);

    /// <summary>
    /// Присоединяет к игре
    /// </summary>
    /// <param name="request">Запрос</param>
    /// <param name="joinKey">Ключ присоединения</param>
    /// <returns><inheritdoc cref="GameDto"/></returns>
    Task<GameDto> JoinGameAsync(JoinRequest request, Guid joinKey);

    /// <summary>
    /// Делает ход
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="moveRequest">Запрос</param>
    /// <param name="accessKey">Ключ доступа</param>
    /// <param name="showBoard">Флаг показа доски</param>
    /// <returns><inheritdoc cref="GameDto"/></returns>
    Task<GameDto> MoveAsync(long gameId, MoveRequest moveRequest, Guid accessKey, bool showBoard);
}
