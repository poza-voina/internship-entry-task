namespace InternshipEntryTask.Core.Data.Game;

/// <summary>
/// Модель описывающая настроки игры
/// </summary>
public class GameSettings
{
    /// <summary>
    /// Высота доски
    /// </summary>
    public int Height { get; set; }
    
    /// <summary>
    /// Ширина доски
    /// </summary>
    public int Width { get; set; }
    
    /// <summary>
    /// Условие выигрыша
    /// </summary>
    public int WinLength { get; set; }

    /// <summary>
    /// Шанс смены значения в ячейке в процентах
    /// </summary>
    public int Chance { get; set; }

    /// <summary>
    /// Номер хода к которому применяется шанс смены значения на доске
    /// </summary>
    public int ChanceTurnNumber { get; set; }
}
