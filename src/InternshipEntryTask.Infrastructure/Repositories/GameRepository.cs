using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Abstractions.Exceptions;
using InternshipEntryTask.Infrastructure.Models;
using InternshipEntryTask.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InternshipEntryTask.Infrastructure.Repositories;

///<inheritdoc/>
public class GameRepository : Repository<GameModel>, IGameRepository
{
    private readonly ApplicationDbContext _dbContext;

    /// <summary>
    /// Создает экземпляр <inheritdoc cref="GameRepository"/>
    /// </summary>
    /// <param name="dbContext"><inheritdoc cref="ApplicationDbContext"/></param>
    public GameRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
        _dbContext = dbContext;
    }

    /// <inheritdoc/>
    public async Task<GameModel> SetGameNewStateAsync(GameModel gameModel, MoveModel moveModel)
	{
        _dbContext.Set<GameModel>().Update(gameModel);
        _dbContext.Set<MoveModel>().Add(moveModel);

		await _dbContext.SaveChangesAsync();

		var updatedGame = await _dbContext.Set<GameModel>()
			.Include(g => g.Moves)
			.FirstOrDefaultAsync(g => g.Id == gameModel.Id) ?? throw new EntityNotFoundException(MessagesConstants.GAME_NOTFOUND_ERROR_MESSAGE_FORMAT);

		return updatedGame;
	}
}
