using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Core.Data.Game;

public class JoinRequest
{
    public required CellValue PlayerSymbol { get; set; }
}