using InternshipEntryTask.Core.Data.Game;

namespace InternshipEntryTask.Core.Game.Interfaces;

/// <summary>
/// Вычислитель игровой доски
/// </summary>
public interface IGameBoardEvaluator
{
    /// <summary>
    /// Сделать ход
    /// </summary>
    /// <returns>Результат хода <inheritdoc cref="BoardResultDto"/></returns>
    public BoardResultDto Move();
}
