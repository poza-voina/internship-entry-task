using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Core.Data.Game;

/// <summary>
/// Модель запроса на присоединение к игре
/// </summary>
public class JoinRequest
{
    /// <summary>
    /// Символ за который игрок хочет играть
    /// </summary>
    public required CellValue PlayerSymbol { get; set; }
}