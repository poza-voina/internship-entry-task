using InternshipEntryTask.Infrastructure.Enums;

namespace InternshipEntryTask.Core.Data.Game;

/// <summary>
/// Модель Dto "Результат обработки хода"
/// </summary>
public class BoardResultDto
{
    /// <summary>
    /// Значение которое поставилось
    /// </summary>
    public CellValue CellValue { get; set; }
    
    /// <summary>
    /// Индекс хода
    /// </summary>
    public int MoveIndex { get; set; }
    
    /// <summary>
    /// Значение следующего хода
    /// </summary>
    public CellValue NextMove { get; set; }

    /// <summary>
    /// Индекс строки в которой был выполнен ход
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Индекс колонки в которой был выполнен ход
    /// </summary>
    public int Column { get; set; }

    /// <summary>
    /// Статус выигрыша
    /// </summary>
    public WinnerStatus? Winner { get; set; }
}