using InternshipEntryTask.Core.Data.Game;

namespace InternshipEntryTask.Core.Game.Interfaces;

public interface IGameBoardEvaluator
{
    public BoardResultDto Move();
}
