using InternshipEntryTask.Core.Data.Game;

namespace InternshipEntryTask.Core.Services.Interfaces;

/// <summary>
/// Сервис для работы с ETag
/// </summary>
public interface IETagService
{
    /// <summary>
    /// Проверяет ETag
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="moveRequest">Запрос на ход</param>
    /// <returns></returns>
    bool Check(long gameId, MoveRequest moveRequest, out string etag);

    /// <summary>
    /// Получить ETag
    /// </summary>
    /// <param name="gameId">Идентификатор Игры</param>
    /// <returns></returns>
    string GetETag(long gameId);

    /// <summary>
    /// Устанваливает ETag
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="moveDto">Модель дто хода</param>
    void SetLastMoveETag(long gameId, MoveDto moveDto);
}
