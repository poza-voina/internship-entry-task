using InternshipEntryTask.Abstractions.Constants;
using InternshipEntryTask.Abstractions.Exceptions;
using InternshipEntryTask.Api.Extenctions;
using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Core.Game.Interfaces;
using InternshipEntryTask.Core.Mappers;
using InternshipEntryTask.Core.Services.Interfaces;
using InternshipEntryTask.Infrastructure.Enums;
using InternshipEntryTask.Infrastructure.Models;
using InternshipEntryTask.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.ComponentModel;

namespace InternshipEntryTask.Core.Services;

public class GameService(
    IRepository<GameModel> gameRepository,
    IRepository<MoveModel> moveRepository,
    IETagService etagService,
    IConfiguration configuration,
    IGameBoardEvaluatorFactory gameBoardFactory)
    : IGameService
{
    private GameSettings GameSettings =>  
        configuration.GetRequiredSection(EnviromentConstants.GAME_SECTION_NAME)
        .Get<GameSettings>()!;

    public async Task<GameDto> CreateGameAsync()
    {
        var gameSettings = GameSettings;
        var gameModel = new GameModel
        {
            NextMove = CellValue.X,
            Status = GameStatus.Progress,
            Width = GameSettings.Width,
            Height = GameSettings.Height,
            JoinKey = Guid.NewGuid(),
        };

        gameModel = await gameRepository.AddAsync(gameModel);

        return gameModel.ToGameDto()
            .WithJoinKey(gameModel.JoinKey);
    }

    public async Task<GameDto> GetGameAsync(long gameId, bool showBoard)
    {
        var query = gameRepository.GetAll();
        if (showBoard)
        {
            query = query.Include(x => x.Moves);
        }

        var gameModel = await query.FirstOrDefaultAsync(x => x.Id == gameId) ??
            throw new EntityNotFoundException(string.Format(MessagesConstants.GAME_NOTFOUND_ERROR_MESSAGE_FORMAT, gameId));

        var result = gameModel.ToGameDto();

        return showBoard ? result.WithBoard(gameModel.Moves, gameModel.Width, gameModel.Height) : result;
    } 

    public async Task<GameDto> JoinGameAsync(JoinRequest request, Guid joinKey)
    {
        var model = await gameRepository.GetAll()
            .FirstOrDefaultAsync(x => x.JoinKey == joinKey)
            ?? throw new EntityNotFoundException(string.Format(MessagesConstants.GAME_NOTFOUND_ERROR_MESSAGE_FORMAT, joinKey));

        var accessKey = Guid.NewGuid();
        switch (request.PlayerSymbol)
        {
            case CellValue.X:
                if (model.AccessKeyPlayerX is { })
                {
                    throw new BaseGameException(MessagesConstants.PLAYER_X_ALREADY_EXISTS_MESSAGE);
                }
                model.AccessKeyPlayerX = accessKey;
                break;

            case CellValue.O:
                if (model.AccessKeyPlayerO is { })
                {
                    throw new BaseGameException(MessagesConstants.PLAYER_O_ALREADY_EXISTS_MESSAGE);
                }
                model.AccessKeyPlayerO = accessKey;
                break;

            default:
                throw new InvalidEnumArgumentException(nameof(request.PlayerSymbol), (int)request.PlayerSymbol, typeof(CellValue));
        }

        await gameRepository.UpdateAsync(model);

        return model.ToGameDto()
            .WithAccessKey(accessKey);
    }

    public async Task<GameDto> MoveAsync(long gameId, MoveRequest moveRequest, Guid accessKey, bool showBoard)
    {
        var winLength = configuration.GetRequiredSection(EnviromentConstants.GAME_SECTION_NAME)
            .GetRequiredValue<int>(EnviromentConstants.GAME_WINLENGTH_NAME);

        var gameModel = await GetGameByIdAndAccessKeyAsync(gameId, accessKey);

        ValidateGameStatus(gameModel);
        ValidatePlayerMove(gameModel, accessKey);

        var moveResult = ProccessMove(gameModel, winLength, moveRequest.Row, moveRequest.Column);

        var result = await ApplyResultAsync(gameModel, moveResult, accessKey);

        etagService.SetLastMoveETag(gameId, moveResult.ToMoveDto());

        return showBoard ? result.WithBoard(gameModel.Moves, gameModel.Width, gameModel.Height) : result;
    }

    private async Task<GameDto> ApplyResultAsync(GameModel gameModel, BoardResultDto moveResult, Guid accessKey)
    {
        var move = moveResult.ToMoveModel(accessKey, gameModel.Id);
        await moveRepository.AddAsync(move);

        gameModel.NextMove = moveResult.NextMove;
        if (moveResult.Winner is { })
        {
            gameModel.Status = GameStatus.Finished;
        }
        gameModel.Winner = moveResult.Winner;
        
        gameModel.Moves.Add(move);
        await gameRepository.UpdateAsync(gameModel);

        return gameModel.ToGameDto();
    }

    private BoardResultDto ProccessMove(GameModel gameModel, int winLength, int row, int column)
    {
        var gameSettings = GameSettings;
        gameSettings.Width = gameModel.Width;
        gameSettings.Height = gameModel.Height;

        var gameBoard = gameBoardFactory.Create(new BoardDto
        {
            NextMove = gameModel.NextMove,
            Moves = gameModel.Moves,
            GameSettings = GameSettings,
            Row = row,
            Column = column,
        });

        return gameBoard.Move();
    }

    private async Task<GameModel> GetGameByIdAndAccessKeyAsync(long gameId, Guid accessKey)
    {
        var game = await gameRepository
            .GetAll()
            .Include(x => x.Moves)
            .FirstOrDefaultAsync(
                x => gameId == x.Id &&
                (
                    x.AccessKeyPlayerX == accessKey ||
                    x.AccessKeyPlayerO == accessKey)
                );

        if (game is null)
        {
            throw new EntityNotFoundException(MessagesConstants.GAME_NOT_FOUND_WITH_ACCESSKEY_ERROR_MESSAGE);
        }

        return game;
    }

    private void ValidatePlayerMove(GameModel game, Guid accessKey)
    {
        bool isCurrentPlayerMove = (game.NextMove == CellValue.X && game.AccessKeyPlayerX == accessKey) ||
                                  (game.NextMove == CellValue.O && game.AccessKeyPlayerO == accessKey);

        if (!isCurrentPlayerMove)
        {
            throw new BaseGameException(MessagesConstants.NOT_YOUR_TURN_ERROR_MESSAGE);
        }
    }
    
    private void ValidateGameStatus(GameModel gameModel)
    {
        if (gameModel.Status == GameStatus.Finished)
        {
            throw new BaseGameException(MessagesConstants.GAME_IS_FINISHED_ERROR_MESSAGE);
        }
    }
}
