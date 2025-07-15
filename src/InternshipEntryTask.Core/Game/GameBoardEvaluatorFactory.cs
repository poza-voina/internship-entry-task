using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Core.Game.Interfaces;

namespace InternshipEntryTask.Core.Game;

/// <inheritdoc/>
public class GameBoardEvaluatorFactory : IGameBoardEvaluatorFactory
{
    /// <inheritdoc/>
    public IGameBoardEvaluator Create(BoardDto dto) =>
        new GameBoardEvaluator(dto);
}
