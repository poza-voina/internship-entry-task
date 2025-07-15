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
    /// <param name="accessKey">Ключ доступа к ходу</param>
    /// <param name="moveRequest">Запрос на ход</param>
    /// <param name="etag"></param>
    /// <returns></returns>
    bool Check(long gameId, Guid accessKey, MoveRequest moveRequest, out string etag);

    /// <summary>
    /// Получить ETag
    /// </summary>
    /// <param name="gameId">Идентификатор Игры</param>
    /// <param name="accessKey">Ключ доступа к ходу</param>
    /// <returns></returns>
    string GetETag(long gameId, Guid accessKey);

    /// <summary>
    /// Устанваливает ETag
    /// </summary>
    /// <param name="gameId">Идентификатор игры</param>
    /// <param name="accessKey">Ключ доступа к ходу</param>
    /// <param name="moveDto">Модель дто хода</param>
    void SetLastMoveETag(long gameId, Guid accessKey, MoveDto moveDto);
}
