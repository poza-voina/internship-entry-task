using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Core.Data.Game;

public class MoveDto
{
    public int Row { get; set; } 
    public int Column { get; set; }
    public CellValue CellValue { get; set; }
}