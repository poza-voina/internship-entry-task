using InternshipEntryTask.Infrastructure.Enums;
using InternshipEntryTask.Infrastructure.Models;

namespace InternshipEntryTask.Core.Data.Game;

/// <summary>
/// Модель ДТО для проверки хода в игре
/// </summary>
public class BoardDto
{
    /// <summary>
    /// Следущий знак
    /// </summary>
    public CellValue NextMove { get; set; }
    
    /// <summary>
    /// Список ходов
    /// </summary>
    public required IEnumerable<MoveModel> Moves { get; set; }
    
    /// <summary>
    /// Индекс строки хода
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Индекс колонки хода
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Настройки игры
    /// </summary>
    public required GameSettings GameSettings { get; set; }
}