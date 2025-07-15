using InternshipEntryTask.Infrastructure.Models;

namespace InternshipEntryTask.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Репозиторий для работы с игрой
/// </summary>
public interface IGameRepository : IRepository<GameModel>
{
    /// <summary>
    /// Изменяет состояние игры
    /// </summary>
    /// <param name="gameModel"><inheritdoc cref="GameModel"/></param>
    /// <param name="moveModel"><inheritdoc cref="MoveModel"/></param>
    /// <returns></returns>
    Task<GameModel> SetGameNewStateAsync(GameModel gameModel, MoveModel moveModel);
}
