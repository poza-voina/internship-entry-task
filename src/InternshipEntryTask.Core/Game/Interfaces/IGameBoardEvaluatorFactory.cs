using InternshipEntryTask.Core.Data.Game;

namespace InternshipEntryTask.Core.Game.Interfaces;

public interface IGameBoardEvaluatorFactory
{
    IGameBoardEvaluator Create(BoardDto dto);
}
