using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Enums;
using InternshipEntryTask.Infrastructure.Models;
using System.ComponentModel;

namespace InternshipEntryTask.Core.Mappers;

/// <summary>
/// Набор методов для маппинга связанного с игрой
/// </summary>
public static class GameMapper
{
    private const char CELL_VALUE_X = 'X';
    private const char CELL_VALUE_O = 'O';
    private const char CELL_VALUE_EMPTY = '_';

    /// <summary>
    /// Маппит в dto <inheritdoc cref="GameDto"/>
    /// </summary>
    /// <param name="gameModel"><inheritdoc cref="GameModel"/></param>
    /// <returns><inheritdoc cref="GameDto"/></returns>
    public static GameDto ToGameDto(this GameModel gameModel) =>
        new GameDto
        {
            Id = gameModel.Id,
            NextMove = gameModel.NextMove,
            Status = gameModel.Status,
            Winner = gameModel.Winner,
            Width = gameModel.Width,
            Height = gameModel.Height,
            Moves = gameModel.Moves.ToMoveDto(),
        };


    /// <summary>
    /// Добавляет ключ присоединения к игре
    /// </summary>
    /// <param name="gameDto"><inheritdoc cref="GameDto"/></param>
    /// <param name="joinKey">Ключ присоединения</param>
    /// <returns><inheritdoc cref="GameDto"/></returns>
    public static GameDto WithJoinKey(this GameDto gameDto, Guid joinKey)
    {
        gameDto.JoinKey = joinKey;
        return gameDto;
    }


    /// <summary>
    /// Добавляет ключ доступа к игре
    /// </summary>
    /// <param name="gameDto"><inheritdoc cref="GameDto"/></param>
    /// <param name="accessKey">Ключ доступа</param>
    /// <returns><inheritdoc cref="GameDto"/></returns>
    public static GameDto WithAccessKey(this GameDto gameDto, Guid accessKey)
    {
        gameDto.AccessKey = accessKey;
        return gameDto;
    }


    /// <summary>
    /// Добавляет доску к игре
    /// </summary>
    /// <param name="gameDto"><inheritdoc cref="GameDto"/></param>
    /// <param name="moves">Коллекция ходов</param>
    /// <param name="width">Ширина доски</param>
    /// <param name="height">Высота доски</param>
    /// <returns>><inheritdoc cref="GameDto"/></returns>
    public static GameDto WithBoard(this GameDto gameDto, IEnumerable<MoveModel> moves, int width, int height)
    {
        gameDto.Board = GetBoard(moves, width, height);
        return gameDto;
    }

    private static char[][] GetBoard(IEnumerable<MoveModel> moves, int width, int height)
    {
        ArgumentNullException.ThrowIfNull(moves);

        var array = FillBoard(width, height);

        foreach (var item in moves)
        {
            array[item.Row][item.Column] = item.CellValue switch
            {
                CellValue.X => CELL_VALUE_X,
                CellValue.O => CELL_VALUE_O,
                _ => throw new InvalidEnumArgumentException(nameof(item.CellValue), (int)item.CellValue, typeof(CellValue))
            };
        }

        return array;
    }

    private static char[][] FillBoard(int width, int height)
    {
        char[][] array = Enumerable
            .Range(0, height)
            .Select(_ => Enumerable.Repeat(CELL_VALUE_EMPTY, width).ToArray())
            .ToArray();

        return array;
    }
}