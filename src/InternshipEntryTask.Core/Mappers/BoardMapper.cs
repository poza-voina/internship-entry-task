using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Enums;
using InternshipEntryTask.Infrastructure.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace InternshipEntryTask.Core.Mappers;

/// <summary>
/// Набор методов расширений для маппинга связанного с доской
/// </summary>
public static class BoardMapper
{
    /// <summary>
    /// Маппит резьтат в модель хода
    /// </summary>
    /// <param name="dto"><inheritdoc cref="BoardResultDto"/></param>
    /// <param name="accessPlayer">Ключ доступа пользователя</param>
    /// <param name="gameId">Идентификатор игры</param>
    /// <returns><inheritdoc cref="MoveModel"/></returns>
    public static MoveModel ToMoveModel(this BoardResultDto dto, Guid accessPlayer, long gameId)
    {
        return new MoveModel
        {
            AccessKeyPlayer = accessPlayer,
            GameId = gameId,
            CellValue = dto.CellValue,
            MoveIndex = dto.MoveIndex,
            Row = dto.Row,
            Column = dto.Column,
        };
    }

    /// <summary>
    /// Маппит резьтат в dto хода
    /// </summary>
    /// <param name="dto"><inheritdoc cref="BoardResultDto"/></param>
    /// <returns><inheritdoc cref="MoveDto"/></returns>
    public static MoveDto ToMoveDto(this BoardResultDto dto) =>
        new MoveDto
        {
            Row = dto.Row,
            Column = dto.Column
        };
}
