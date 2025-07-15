using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Models;

namespace InternshipEntryTask.Core.Mappers;

/// <summary>
/// Набор методов для маппинга ходов
/// </summary>
public static class MoveMapper
{

    /// <summary>
    /// Маппит модель в dto
    /// </summary>
    /// <param name="moveModel"><inheritdoc cref="MoveModel"/></param>
    /// <returns><inheritdoc cref="MoveDto"/></returns>
    public static MoveDto ToMoveDto(this MoveModel moveModel) => 
        new MoveDto
        {
            Row = moveModel.Row,
            Column = moveModel.Column,
            CellValue = moveModel.CellValue,
        };

    /// <summary>
    /// Маппит запрос в dto
    /// </summary>
    /// <param name="moveModel"><inheritdoc cref="MoveRequest"/></param>
    /// <returns><inheritdoc cref="MoveDto"/></returns>
    public static MoveDto ToMoveDto(this MoveRequest moveModel) => new MoveDto
    {
        Row = moveModel.Row,
        Column = moveModel.Column
    };


    /// <summary>
    /// Маппит коллекцию моделей в коллекцию dto
    /// </summary>
    /// <param name="moveModels"><inheritdoc cref="MoveModel"/></param>
    /// <returns><inheritdoc cref="MoveDto"/></returns>
    public static IEnumerable<MoveDto> ToMoveDto(this IEnumerable<MoveModel> moveModels) =>
        moveModels.Select(x => x.ToMoveDto());
}
