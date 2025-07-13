using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Core.Game.Interfaces;

namespace InternshipEntryTask.Core.Game;

public class GameBoardEvaluatorFactory : IGameBoardEvaluatorFactory
{
    public IGameBoardEvaluator Create(BoardDto dto) =>
        new GameBoardEvaluator(dto);
}
