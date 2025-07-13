
using InternshipEntryTask.Core.Data.Game;

namespace InternshipEntryTask.Core.Services.Interfaces;

public interface IGameService
{
    Task<GameDto> CreateGameAsync();
    Task<GameDto> GetGameAsync(long gameId, bool showBoard);
    Task<GameDto> JoinGameAsync(JoinRequest request, Guid joinKey);
    Task<GameDto> MoveAsync(MoveRequest moveRequest, Guid accessKey, bool showBoard);
}
