using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Core.Data.Game;

/// <summary>
/// Модель DTO "Ход"
/// </summary>
public class MoveDto
{
    /// <summary>
    /// Индекс строки хода
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Индекс колонки хода
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Значение в ячейке
    /// </summary>
    public CellValue CellValue { get; set; }
}