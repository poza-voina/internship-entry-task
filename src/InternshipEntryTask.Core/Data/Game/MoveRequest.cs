namespace InternshipEntryTask.Core.Data.Game;

/// <summary>
/// Запрос на ход
/// </summary>
public class MoveRequest
{
    /// <summary>
    /// Индекс строки хода
    /// </summary>
    public int Row { get; set; }

    /// <summary>
    /// Индекс колонки хода
    /// </summary>
    public int Column { get; set; }
}

