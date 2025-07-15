using InternshipEntryTask.Core.Data.Game;

namespace InternshipEntryTask.Core.Game.Interfaces;

/// <summary>
/// Фабрика вычислителей
/// </summary>
public interface IGameBoardEvaluatorFactory
{
    /// <summary>
    /// Создать вычислитель
    /// </summary>
    /// <param name="dto">Доска <inheritdoc cref="BoardDto"/></param>
    /// <returns>Вычислитель <inheritdoc cref="IGameBoardEvaluator"/></returns>
    IGameBoardEvaluator Create(BoardDto dto);
}
