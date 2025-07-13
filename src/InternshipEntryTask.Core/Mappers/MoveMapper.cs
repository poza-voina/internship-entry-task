using InternshipEntryTask.Core.Data.Game;
using InternshipEntryTask.Infrastructure.Models;

namespace InternshipEntryTask.Core.Mappers;

public static class MoveMapper
{
    public static MoveDto ToMoveDto(this MoveModel moveModel) => 
        new MoveDto
        {
            Row = moveModel.Row,
            Column = moveModel.Column
        };

    public static IEnumerable<MoveDto> ToMoveDto(this IEnumerable<MoveModel> moveModels) =>
        moveModels.Select(x => x.ToMoveDto());
}
