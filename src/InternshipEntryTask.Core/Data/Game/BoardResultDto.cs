using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Core.Data.Game;

public class BoardResultDto
{
    public CellValue CellValue { get; set; }
    public int MoveIndex { get; set; }
    public CellValue NextMove { get; set; }
    public int Row { get; set; }
    public int Column { get; set; }
    public WinnerStatus? Winner { get; set; }
}