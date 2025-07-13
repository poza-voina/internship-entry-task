using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Enums;
using InternshipEntryTask.Infrastructure.Models;

namespace InternshipEntryTask.Core.Mappers;

public static class BoardMapper
{
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
}
